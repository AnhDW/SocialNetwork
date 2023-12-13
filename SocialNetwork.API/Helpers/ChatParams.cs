namespace SocialNetwork.API.Helpers
{
    public class ChatParams : PaginationParams
    {
        public string? Username { get; set; }
        public string Container { get; set; } = "Unread";
    }
}
