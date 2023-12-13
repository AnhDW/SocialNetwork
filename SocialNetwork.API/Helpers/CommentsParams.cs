namespace SocialNetwork.API.Helpers
{
    public class CommentsParams : PaginationParams
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Predicate { get; set; }
    }
}
