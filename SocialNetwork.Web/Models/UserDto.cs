namespace SocialNetwork.Web.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Token { get; set; }
        public required string AvatarUrl { get; set; }
        public required string KnownAs { get; set; }
        public required string Gender { get; set; }
    }
}
