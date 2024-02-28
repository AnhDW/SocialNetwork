using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;

namespace SocialNetwork.Web.Service
{
    public class AccountService : IAccountService
    {
        private readonly IBaseService _baseService;

        public AccountService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto?> Login(LoginDto loginDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginDto,
                Url = SD.SocialNetworkAPIBase + "/api/account/login"
            };
            return _baseService.SendAsync(request, withBearer: false)!;
        }

        public Task<ResponseDto?> Register(RegisterDto registerDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registerDto,
                Url = SD.SocialNetworkAPIBase + "/api/account/register"
            };
            return _baseService.SendAsync(request, withBearer: false)!;
        }
    }
}
