namespace SocialNetwork.Web.Models
{
    public class CommentDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string Countdown { get; set; }
        public OwnerDto User { get; set; }
    }
}