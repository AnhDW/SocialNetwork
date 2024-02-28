using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class RoomMembersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IRoomRepo _roomRepo;
        private readonly IRoomMemberRepo _memberRepo;

        public RoomMembersController(IUserRepo userRepo, IRoomRepo roomRepo, IRoomMemberRepo memberRepo)
        {
            _userRepo = userRepo;
            _roomRepo = roomRepo;
            _memberRepo = memberRepo;
        }

        [HttpPost("invite-to-room/{roomId}/{username}")]
        public async Task<ActionResult> JoinRoom(int roomId, string username)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());
            var invitedPerson = await _userRepo.GetByUsernameAsync(username);
            var room = await _roomRepo.GetRoom(roomId);
            var member = await _memberRepo.GetRoomMember(roomId, user.Id);
            var newMember = await _memberRepo.GetRoomMember(roomId, invitedPerson.Id);
            if (room == null) return NotFound();
            if (member == null) return BadRequest("You are not a member of this chat room");
            if (newMember != null) return BadRequest("You are already a member of the chat room");

            newMember = new RoomMember
            {
                RoomId = roomId,
                MemberId = invitedPerson.Id,
            };
            
            _memberRepo.JoinRoom(newMember);

            if (await _userRepo.SaveAllAsync()) return Ok("Join the room successfully.");

            return BadRequest("Problem join room.");
        }

        [HttpDelete("leave-room/{roomId}")]
        public async Task<ActionResult> LeaveRoom(int roomId)
        {
            var currentUserId = User.GetUserId();
            var member = await _memberRepo.GetRoomMember(roomId, currentUserId);
            if (member == null) return NotFound();

            _memberRepo.LeaveRoom(member);

            if (await _userRepo.SaveAllAsync()) return Ok("Successfully left the room.");

            return BadRequest("Problem leave room.");
        }
    }
}
