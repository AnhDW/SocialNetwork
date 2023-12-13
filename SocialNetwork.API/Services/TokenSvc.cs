using Microsoft.IdentityModel.Tokens;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.API.Services
{
    public class TokenSvc : ITokenSvc
    {
        private readonly SymmetricSecurityKey _key;
        public TokenSvc(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHander = new JwtSecurityTokenHandler();
            var token = tokenHander.CreateToken(tokenDescriptor);

            return tokenHander.WriteToken(token);
        }
    }
}
