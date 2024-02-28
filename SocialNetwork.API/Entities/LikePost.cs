namespace SocialNetwork.API.Entities
{
    public class LikePost
    {
        public int UserId {  get; set; }
        public int PostId {  get; set; }
        public string ReationType { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
