using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IChatRepo
    {
        void AddChat(Chat chat);
        void DeleteChat(Chat chat);
        Task<Chat> GetChat(int id);
        Task<PagedList<ChatDto>> GetChatsForUser(ChatParams chatParams);
        Task<IEnumerable<ChatDto>> GetChatsThread(string currentUsername, string reciptentUsername);
        Task<bool> SaveAllAsync();
    }
}
