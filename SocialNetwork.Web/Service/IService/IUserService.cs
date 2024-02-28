using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDto?> GetTimeline(int userId);
        Task<ResponseDto?> Logout();
    }
}
