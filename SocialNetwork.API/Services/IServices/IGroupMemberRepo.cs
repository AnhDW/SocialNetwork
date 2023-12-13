using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Services.IServices
{
    public interface IGroupMemberRepo
    {
        Task<GroupMember> GetGroupMember(int groupId, int memberId);
        void JoinGroup(GroupMember groupMember);
        void LeaveGroup(GroupMember groupMember);
    }
}
