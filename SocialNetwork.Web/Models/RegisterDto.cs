namespace SocialNetwork.Web.Models
{
    public class RegisterDto
    {
        public required string Username { get; set; }

        public required string Email { get; set; }
        public required string KnownAs { get; set; }
        public required string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public required string Password { get; set; }
    }
}
