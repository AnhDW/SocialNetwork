using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IGroupRepo
    {
        Task<PagedList<GroupDto>> GetGroups(GroupParams groupParams);
        Task<Group> GetGroup(int id);
    }
}
