namespace SocialNetwork.API.Helpers
{
    public class PostParams : PaginationParams
    {
        public int? UserId { get; set; }
        public string? Content {  get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}
