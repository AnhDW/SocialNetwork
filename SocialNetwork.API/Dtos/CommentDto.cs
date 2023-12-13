namespace SocialNetwork.API.Dtos
{
    public class CommentDto
    {
        public int PostId { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
