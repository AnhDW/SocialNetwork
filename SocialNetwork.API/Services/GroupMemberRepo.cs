using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class GroupMemberRepo : IGroupMemberRepo
    {
        private readonly DataContext _context;

        public GroupMemberRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<GroupMember> GetGroupMember(int groupId, int memberId)
        {
            return await _context.GroupMembers.FirstOrDefaultAsync(g => g.GroupId == groupId && g.MemberId == memberId);
        }

        public void JoinGroup(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
        }

        public void LeaveGroup(GroupMember groupMember)
        {
            _context.GroupMembers.Remove(groupMember);
        }
    }
}
