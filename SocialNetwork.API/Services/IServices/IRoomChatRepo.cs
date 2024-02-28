using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IRoomChatRepo
    {
        void AddMessage(RoomChat roomChat);
        void DeleteMessage(RoomChat roomChat);
        Task<RoomChat> GetMessageInRoomChat(int id);
        Task<PagedList<RoomChatDto>> GetRoomChats(RoomChatParams roomChatParams);
        Task<bool> SaveAllAsync();
    }
}
