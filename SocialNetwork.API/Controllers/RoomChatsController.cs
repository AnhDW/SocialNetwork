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
    public class RoomChatsController : ControllerBase
    {
        private readonly IRoomChatRepo _roomChatRepo;
        private readonly IRoomMemberRepo _roomMemberRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public RoomChatsController(IRoomChatRepo roomChatRepo, IUserRepo userRepo, IMapper mapper, IRoomMemberRepo roomMemberRepo)
        {
            _roomChatRepo = roomChatRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _roomMemberRepo = roomMemberRepo;
        }

        [HttpGet()]
        public async Task<ActionResult<PagedList<RoomChatDto>>> GetRoomChats([FromQuery] RoomChatParams roomChatParams)
        {
            var currentUserId = User.GetUserId();
            var roomMember = await _roomMemberRepo.GetRoomMember(roomChatParams.RoomId, currentUserId);
            if (roomMember == null) return NotFound();

            var roomChats = await _roomChatRepo.GetRoomChats(roomChatParams);

            Response.AddPaginationHeader(new PaginationHeader(roomChats.CurrentPage, roomChats.PageSize, roomChats.TotalCount, roomChats.TotalPages));

            return Ok(roomChats);
        }

        [HttpPost("create-message")]
        public async Task<ActionResult<RoomChatDto>> CreateMessage([FromBody]CreateMessageInRoomDto createMessageInRoomDto)
        {
            var currentUserId = User.GetUserId();
            var roomMember = await _roomMemberRepo.GetRoomMember(createMessageInRoomDto.RoomId, currentUserId);
            if(roomMember == null) return BadRequest();

            var roomChat = new RoomChat
            {
                UserId = currentUserId,
                RoomId = createMessageInRoomDto.RoomId,
                Message = createMessageInRoomDto.Message,
            };

            _roomChatRepo.AddMessage(roomChat);

            if(await _roomChatRepo.SaveAllAsync()) return Ok(_mapper.Map<RoomChatDto>(roomChat));

            return BadRequest("Problem create message");
        }

        [HttpDelete("delete-message/{roomChatId}")]
        public async Task<ActionResult> DeleteMessage(int roomChatId)
        {
            var currentUserId = User.GetUserId();
            var roomChat = await _roomChatRepo.GetMessageInRoomChat(roomChatId);
            if (currentUserId != roomChat.UserId) return BadRequest("You cannot delete this message");

            _roomChatRepo.DeleteMessage(roomChat);

            if (await _roomChatRepo.SaveAllAsync()) return Ok("Delete successfully");

            return BadRequest("Problem delete roomchat");
        }
    }
}
