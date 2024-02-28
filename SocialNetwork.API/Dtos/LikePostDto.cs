namespace SocialNetwork.API.Dtos
{
    public class LikePostDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string ReationType { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public OwnerDto User { get; set; }
    }
}
