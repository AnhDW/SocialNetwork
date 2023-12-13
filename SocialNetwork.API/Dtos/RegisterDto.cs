using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Dtos
{
    public class RegisterDto
    {
        public string Username { get; set; }

        public string Email { get; set; }
        public string KnownAs { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
        public string Password { get; set; }
    }
}