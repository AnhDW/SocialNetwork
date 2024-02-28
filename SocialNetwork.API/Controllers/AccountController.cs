using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Entities;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenSvc _tokenSvc;
        private readonly IMapper _mapper;

        public AccountController(DataContext context, ITokenSvc tokenSvc, IMapper mapper)
        {
            _context = context;
            _tokenSvc = tokenSvc;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto register)
        {
            if(await UserExists(register.Username)) return BadRequest("Username is taken");
            var user = _mapper.Map<User>(register);

            using var hmac = new HMACSHA512();

            user.UserName = register.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            user.PasswordSalt = hmac.Key;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto {
                Id = user.Id,
                Username = user.UserName,
                Token = _tokenSvc.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login.Username);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for(var i = 0;i< computerHash.Length; i++)
            {
                if (computerHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = _tokenSvc.CreateToken(user),
                KnownAs = user.KnownAs,
                AvatarUrl = user.AvatarUrl,
                Gender = user.Gender,
            };
        }

        [HttpPut("tokens")]
        public async Task<ActionResult> GetTokens()
        {
            var tokens = await _tokenSvc.GetTokensInActive();
            
            return Ok(tokens);
        }
        
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName == username);
        }
    }
}
