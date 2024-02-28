using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IRoomRepo
    {
        Task<PagedList<InteractWithRoomDto>> GetRooms(RoomParams roomParams);
        Task<Room> GetRoom(int id);
    }
}
