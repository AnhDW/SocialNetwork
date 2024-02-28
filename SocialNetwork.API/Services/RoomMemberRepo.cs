using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class RoomMemberRepo : IRoomMemberRepo
    {
        private readonly DataContext _context;

        public RoomMemberRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<RoomMember> GetRoomMember(int roomId, int memberId)
        {
            return await _context.RoomMembers.FirstOrDefaultAsync(g => g.RoomId == roomId && g.MemberId == memberId);
        }

        public void JoinRoom(RoomMember roomMember)
        {
            _context.RoomMembers.Add(roomMember);
        }

        public void LeaveRoom(RoomMember roomMember)
        {
            _context.RoomMembers.Remove(roomMember);
        }
    }
}
