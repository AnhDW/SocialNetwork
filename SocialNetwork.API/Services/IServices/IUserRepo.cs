using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IUserRepo
    {
        void Update(User user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<PagedList<PersonalPageDto>> GetPersonalPagesAsync(UserParams userParams);
        Task<PersonalPageDto> GetPersonalPageAsync(string username);
        Task<PersonalPageDto> GetPersonalPageByIdAsync(int currentUserId);


    }
}
