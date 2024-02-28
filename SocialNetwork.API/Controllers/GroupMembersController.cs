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
    public class GroupMembersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IGroupRepo _groupRepo;
        private readonly IGroupMemberRepo _memberRepo;

        public GroupMembersController(IUserRepo userRepo, IGroupRepo groupRepo, IGroupMemberRepo memberRepo)
        {
            _userRepo = userRepo;
            _groupRepo = groupRepo;
            _memberRepo = memberRepo;
        }

        [HttpPost("join-group/{groupId}")]
        public async Task<ActionResult> JoinGroup(int groupId)
        {
            var user = await _userRepo.GetByUsernameAsync(User.GetUsername());
            var group = await _groupRepo.GetGroup(groupId);
            var member = await _memberRepo.GetGroupMember(groupId, user.Id);
            if (group == null) return NotFound();
            if (user.Id == group.CreatorId) return BadRequest("This is your group.");
            if (member != null) return BadRequest("You are already a member of the group.");

            member = new GroupMember
            {
                GroupId = groupId,
                MemberId = user.Id,
            };
            
            _memberRepo.JoinGroup(member);

            if (await _userRepo.SaveAllAsync()) return Ok("Join the group successfully.");

            return BadRequest("Problem join group.");
        }

        [HttpDelete("leave-group/{groupId}")]
        public async Task<ActionResult> LeaveGroup(int groupId)
        {
            var currentUserId = User.GetUserId();
            var member = await _memberRepo.GetGroupMember(groupId, currentUserId);
            if (member == null) return NotFound();

            _memberRepo.LeaveGroup(member);

            if (await _userRepo.SaveAllAsync()) return Ok("Successfully left the group.");

            return BadRequest("Problem leave group.");
        }
    }
}
