using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;

namespace SocialNetwork.Web.Service
{
    public class UserService : IUserService
    {
        private readonly IBaseService _baseService;
        private readonly ITokenProvider _tokenProvider;

        public UserService(IBaseService baseService, ITokenProvider tokenProvider)
        {
            _baseService = baseService;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> GetTimeline(int userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                Url = SD.SocialNetworkAPIBase + $"/api/users/timeline/{userId}"
            });
        }

        public async Task<ResponseDto?> Logout()
        {
            var token = _tokenProvider.GetToken();
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.SocialNetworkAPIBase + "/api/users/logout/" + token
            });
        }
    }
}
