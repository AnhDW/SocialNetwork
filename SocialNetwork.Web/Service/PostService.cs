    using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;


namespace SocialNetwork.Web.Service
{
    public class PostService : IPostService
    {
        private readonly IBaseService _baseService;
        public PostService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto?> CreatePostAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetPostListAsync(int? userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.SocialNetworkAPIBase + "/api/posts?userId=" + userId
            });
        }

        public Task<ResponseDto?> GetPostAsync(string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetPostByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto());
        }

        public Task<ResponseDto?> UpdatePostAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }
    }
}
