using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Data;
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
    public class FriendsController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IFriendRepo _friendRepo;

        public FriendsController(IUserRepo userRepo, IFriendRepo friendRepo)
        {
            _userRepo = userRepo;
            _friendRepo = friendRepo;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<FriendDto>>> GetUserFriends([FromQuery]FriendsParams friendsParams)
        {
            friendsParams.UserId = User.GetUserId();
            var users = await _friendRepo.GetUserFriends(friendsParams);
            Response.AddPaginationHeader(new PaginationHeader(
                users.CurrentPage, 
                users.PageSize, 
                users.TotalCount, 
                users.TotalPages));

            return Ok(users);
        }

        [HttpGet("invitations")]
        public async Task<ActionResult<PagedList<FriendRequestDto>>> Invitations()
        {
            var sourceUserId = User.GetUserId();
            var users = await _friendRepo.GetInvitations(sourceUserId);
            Response.AddPaginationHeader(new PaginationHeader(
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages));
            return Ok(users);
        }

        [HttpPost("send-friend-invitation/{username}")]
        public async Task<ActionResult> AddFriend(string username)
        {
            var sourceUserId = User.GetUserId();
            var friendUser = await _userRepo.GetByUsernameAsync(username);//người muốn kết nối
            var sourceUser = await _friendRepo.GetUserWithFriends(sourceUserId);//người dùng hiện tại

            if (friendUser == null) return NotFound();

            var userFriend = await _friendRepo.GetUserFriend(sourceUserId, friendUser.Id);
            if (userFriend != null) return BadRequest();
            
            userFriend = new Friend
            {
                UserId = sourceUserId,
                FriendId = friendUser.Id,
                IsFriend = false
            };

            _friendRepo.Add(userFriend);
            if (await _userRepo.SaveAllAsync())
            {
                return Ok(sourceUser.KnownAs + " sent " + friendUser.KnownAs + " a friend request");
            } 

            return BadRequest("Fail to connect user");
        }

        [HttpPost("accept-invitation/{username}")]
        public async Task<ActionResult> AcceptInvitation(string username)
        {
            var sourceUserId = User.GetUserId();
            var friendUser = await _userRepo.GetByUsernameAsync(username);//người muốn kết nối
            var sourceUser = await _friendRepo.GetUserWithFriends(sourceUserId);//người dùng hiện tại

            if (friendUser == null) return NotFound();

            var checkInvitation = await _friendRepo.GetInvitation(sourceUserId);
            if (checkInvitation == null) return BadRequest();

            checkInvitation.IsFriend = true;
            var friend = new Friend
            {
                UserId = sourceUserId,
                FriendId = friendUser.Id,
                IsFriend = true
            };

            _friendRepo.Add(friend);
            if (await _userRepo.SaveAllAsync())
            {
                return Ok(sourceUser.UserName + "has accepted " + friendUser.UserName + "'s friend request");
            }

            return BadRequest("Fail to connect user");
        }

        [HttpDelete("reject-invitation/{username}")]
        public async Task<ActionResult> RejectInvitation(string username)
        {
            var sourceUserId = User.GetUserId();
            var friendUser = await _userRepo.GetByUsernameAsync(username);
            var sourceUser = await _friendRepo.GetUserWithFriends(sourceUserId);//người dùng hiện tại

            if (friendUser == null) return NotFound();

            var checkInvitation = await _friendRepo.GetUserFriend(friendUser.Id, sourceUserId);
            if (checkInvitation == null || checkInvitation.IsFriend) return BadRequest();

            _friendRepo.Delete(checkInvitation);
            if (await _userRepo.SaveAllAsync())
            {
                return Ok(sourceUser.KnownAs + " refused " + friendUser.KnownAs + "'s friend request");
            }

            return BadRequest("Fail to connect user");
        }

        [HttpDelete("cancel-invitation/{username}")]
        public async Task<ActionResult> CancelInvitation(string username)
        {
            var sourceUserId = User.GetUserId();
            var friendUser = await _userRepo.GetByUsernameAsync(username);

            if (friendUser == null) return NotFound();

            var checkInvitation = await _friendRepo.GetUserFriend(sourceUserId, friendUser.Id);
            if (checkInvitation == null || checkInvitation.IsFriend) return BadRequest();

            _friendRepo.Delete(checkInvitation);
            if (await _userRepo.SaveAllAsync())
            {
                return Ok("cancel successfully");
            }

            return BadRequest("Fail to connect user");
        }

        [HttpDelete("unfriend/{username}")]
        public async Task<ActionResult> Unfriend(string username)
        {
            var sourceUserId = User.GetUserId();
            var friend = await _userRepo.GetByUsernameAsync(username);

            if (friend == null) return NotFound();

            var userFriend = await _friendRepo.GetUserFriend(sourceUserId, friend.Id);
            var friendUser = await _friendRepo.GetUserFriend(friend.Id, sourceUserId);
            if (userFriend == null || !userFriend.IsFriend) return BadRequest();

            _friendRepo.Delete(userFriend);
            _friendRepo.Delete(friendUser);
            if (await _userRepo.SaveAllAsync())
            {
                return Ok("Unfriend successfully");
            }

            return BadRequest("Fail to connect user");
        }
    }
}
