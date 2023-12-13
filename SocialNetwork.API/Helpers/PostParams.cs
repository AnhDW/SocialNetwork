namespace SocialNetwork.API.Helpers
{
    public class PostParams : PaginationParams
    {
        public string? Content {  get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}
