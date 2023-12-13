using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface ILikePostRepo
    {
        Task<LikePost> GetUserLike(int userId, int postId);
        Task<User> GetPostWithLikes(int userId); //đã thích những post nào
        Task<PagedList<InteractWithPostDto>> GetUserLikes(LikesParams likesParams);
        Task Update(LikePost likePost);
        Task Delete(LikePost likePost);
    }
}
