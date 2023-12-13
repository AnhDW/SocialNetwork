namespace SocialNetwork.API.Entities
{
    public class Post : BaseEntity
    {
        public string Content {  get; set; }
        public string ShowMode { get; set; } = "Global"; //Global, Friends, Personal, SpecifiedObject
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public List<Attachment> Attachments { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<LikePost> LikePosts { get; set; } = new();
    }
}
