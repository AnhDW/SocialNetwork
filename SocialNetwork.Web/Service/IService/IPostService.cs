using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Service.IService
{
    public interface IPostService
    {
        Task<ResponseDto?> GetPostAsync(string couponCode);
        Task<ResponseDto?> GetPostListAsync(int? userId);
        Task<ResponseDto?> GetPostByIdAsync(int id);
        Task<ResponseDto?> CreatePostAsync(PostDto postDto);
        Task<ResponseDto?> UpdatePostAsync(PostDto postDto);
        Task<ResponseDto?> DeletePostAsync(int id);

    }
}
