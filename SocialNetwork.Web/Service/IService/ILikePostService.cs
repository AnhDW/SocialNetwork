using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Service.IService
{
    public interface ILikePostService
    {
        Task<ResponseDto?> IsLikePost(int postId);
        Task<ResponseDto?> AddLikePost(int postId, string reactionType = "like");
    }
}
