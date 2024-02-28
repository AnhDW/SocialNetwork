using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.API.Data;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialNetwork.API.Services
{
    public class TokenSvc : ITokenSvc
    {
        private readonly SymmetricSecurityKey _key;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TokenSvc(IConfiguration config, DataContext context, IMapper mapper)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
            _context = context;
            _mapper = mapper;
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

            user.TokenManagements.Add(new TokenManagement { Token = tokenHander.WriteToken(token)});
            _context.SaveChanges();

            return tokenHander.WriteToken(token);
        }

        public async Task<List<TokenManagementDto>> GetTokensInActive()
        {
            var tokens = await _context.TokenManagements.ToListAsync();
            foreach(var token in tokens)
            {
                if (token.Created.CacuateTime() >= 7)
                {
                    token.IsActive = false;
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<List<TokenManagementDto>>(tokens);
        }
    }
}
