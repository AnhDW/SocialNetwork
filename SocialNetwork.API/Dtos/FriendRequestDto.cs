namespace SocialNetwork.API.Dtos
{
    public class FriendRequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
