using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ValidateToken]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPostRepo _postRepo;
        private readonly IGroupRepo _groupRepo;
        private readonly IGroupMemberRepo _groupMemberRepo;
        private readonly IRoomRepo _roomRepo;
        private readonly IRoomMemberRepo _roomMemberRepo;
        private readonly IAttachmentSvc _attachmentSvc;
        private readonly IMapper _mapper;
        private readonly ITokenSvc _tokenSvc;

        public UsersController(IUserRepo userRepo, IAttachmentSvc attachmentSvc, IMapper mapper, IPostRepo postRepo, IGroupRepo groupRepo, IRoomRepo roomRepo, IGroupMemberRepo groupMemberRepo, IRoomMemberRepo roomMemberRepo, ITokenSvc tokenSvc)
        {
            _userRepo = userRepo;
            _attachmentSvc = attachmentSvc;
            _mapper = mapper;
            _postRepo = postRepo;
            _groupRepo = groupRepo;
            _roomRepo = roomRepo;
            _groupMemberRepo = groupMemberRepo;
            _roomMemberRepo = roomMemberRepo;
            _tokenSvc = tokenSvc;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<PersonalPageDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var currentUser = await _userRepo.GetByUsernameAsync(User.GetUsername());
            userParams.CurrentUsername = currentUser.UserName;

            var users = await _userRepo.GetPersonalPagesAsync(userParams);

            Response.AddPaginationHeader(
                new PaginationHeader(
                    users.CurrentPage,
                    users.PageSize,
                    users.TotalCount,
                    users.TotalPages
                    )
                );
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<PersonalPageDto>> GetUser(string username)
        {
            return await _userRepo.GetPersonalPageAsync(username);
        }

        [HttpGet("timeline/{userId}")]
        public async Task<ActionResult<PersonalPageDto>> GetTimeline(int userId)
        {
            return await _userRepo.GetPersonalPageByIdAsync(userId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateProfileDto updateProfile)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());
            if (user == null) return NotFound();
            _mapper.Map(updateProfile, user);

            if (await _userRepo.SaveAllAsync()) return NoContent();
            
            return BadRequest("Failed to update profile");
        }

        
        [HttpPost("create-post")]
        public async Task<ActionResult<PostDto>> CreatePost([FromForm]PostDto postDto, [FromForm] List<IFormFile> files)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var post = new Post()
            {
                Content = postDto.Content,
            };
            
            foreach(var file in files)
            {
                var url = _attachmentSvc.AddAttachment(file);
                var attachment = new Attachment()
                {
                    Url = url,
                    FileType = file.ContentType.Split('/')[0] + "s",
                };
                post.Attachments.Add(attachment);
            }
            
            user.Posts.Add(post);
            if(await _userRepo.SaveAllAsync())
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PostDto>(post));

            return BadRequest("Problem adding post");
        }

        [HttpDelete("delete-post/{postId}")]
        public async Task<ActionResult> DeletePost(int postId)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());
            if (user == null) return NotFound();

            var post = await _postRepo.GetPostById(postId);
            //var post = user.Posts.FirstOrDefault(p => p.Id == postId);

            if (user.Posts.FirstOrDefault(p => p.Id == postId) == null) return BadRequest("Post này có thể không tồn tại hoặc không thuộc quyền sở hữu của bạn");

            if (post == null) return NotFound();

            var attachments = post.Attachments;

            foreach (var attachment in attachments)
            {
                var url = attachment.Url;
                _attachmentSvc.DeleteAttachment(url);
            }

            user.Posts.Remove(post);
            if (await _userRepo.SaveAllAsync())
                return Ok("deleted successfully"); 

            return BadRequest("Problem delete post");
        }

        [HttpPost("create-group")]
        public async Task<ActionResult<GroupDto>> CreateGroup([FromBody] GroupDto groupDto)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var group = new Group
            {
                GroupName = groupDto.GroupName,
                Description = groupDto.Description,
            };
            var groupMember = new GroupMember
            {
                MemberId = user.Id,
                Role = "group master"
            };

            group.GroupMembers.Add(groupMember);
            user.Groups.Add(group);

            if (await _userRepo.SaveAllAsync()) 
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<GroupDto>(group));

            return BadRequest("Problem add group");
        }

        [HttpDelete("delete-group/{groupId}")]
        public async Task<ActionResult> DeleteGroup(int groupId)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var group = await _groupRepo.GetGroup(groupId);

            if (group == null) return NotFound();
            var groupMembers = group.GroupMembers;
            foreach( var member in groupMembers)
            {
                _groupMemberRepo.LeaveGroup(member);
            }

            user.Groups.Remove(group);

            if (await _userRepo.SaveAllAsync()) return Ok("deleted successfully");

            return BadRequest("Problem delete group");

        }

        [HttpPost("create-room")]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] RoomDto roomDto)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();

            var roomMember = new RoomMember
            {
                MemberId = user.Id,
                Role = "room master"
            };
            
            var room = new Room
            {
                RoomName = roomDto.RoomName,
                Description = roomDto.Description,
            };
            
            room.RoomMembers.Add(roomMember);
            user.Rooms.Add(room);

            if(await _userRepo.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName}, _mapper.Map<RoomDto>(room));
            }

            return BadRequest("Problem add room");
        }

        [HttpDelete("delete-room/{roomId}")]
        public async Task<ActionResult> DeleteRoom(int roomId)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var room = await _roomRepo.GetRoom(roomId);

            if (room == null) return NotFound();
            var roomMembers = room.RoomMembers;
            foreach (var member in roomMembers)
            {
                _roomMemberRepo.LeaveRoom(member);
            }
            user.Rooms.Remove(room);

            if (await _userRepo.SaveAllAsync()) return Ok("deleted successfully");

            return BadRequest("Problem delete room");
        }

        [HttpDelete("logout/{token}")]
        public async Task<ActionResult> Logout(string token)
        {
            var user = await _userRepo.GetByIdAsync(User.GetUserId());
            var currentToken = user.TokenManagements.FirstOrDefault(user => user.Token == token);
            user.TokenManagements.Remove(currentToken!); 
            if(await _userRepo.SaveAllAsync()) return Ok("Logout successfully"); 
            return BadRequest("Problem logout user");
        }
    }
}
