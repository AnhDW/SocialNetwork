using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Services.IServices
{
    public interface IRoomMemberRepo
    {
        Task<RoomMember> GetRoomMember(int roomId, int memberId);
        void JoinRoom(RoomMember roomMember);
        void LeaveRoom(RoomMember roomMember);
    }
}
