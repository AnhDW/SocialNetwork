using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Services.IServices
{
    public interface ITokenSvc
    {
        string CreateToken(User user);
        Task<List<TokenManagementDto>> GetTokensInActive();
    }
}
