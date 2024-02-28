using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;

namespace SocialNetwork.Web.Service
{
    public class LikePostService : ILikePostService
    {
        private readonly IBaseService _baseService;

        public LikePostService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddLikePost(int postId, string? reactionType)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = reactionType!,
                Url = SD.SocialNetworkAPIBase + $"/api/likeposts/{postId}"
            });
        }

        public async Task<ResponseDto?> IsLikePost(int postId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.SocialNetworkAPIBase + $"/api/likeposts/is-like/{postId}"
            });
        }
    }
}
