using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Dtos
{
    public class PersonalPageDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
        public string? KnownAs { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        
        public List<PostDto> Posts { get; set; }
    }
}
