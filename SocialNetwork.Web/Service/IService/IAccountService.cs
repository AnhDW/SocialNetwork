using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Service.IService
{
    public interface IAccountService
    {
        Task<ResponseDto?> Login(LoginDto loginDto);
        Task<ResponseDto?> Register(RegisterDto registerDto);
    }
}
