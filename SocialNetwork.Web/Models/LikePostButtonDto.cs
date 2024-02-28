namespace SocialNetwork.Web.Models
{
    public class LikePostButtonDto
    {
        public int PostId { get; set; }
        public bool IsLike { get; set; } = false;
        public string ReactionType { get; set; } = string.Empty;
    }
}
