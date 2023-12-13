using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface IPostRepo
    {
        Task<PagedList<PostDto>> GetPosts(PostParams postParams);
        Task<Post> GetPostById(int postId);
    }
}
