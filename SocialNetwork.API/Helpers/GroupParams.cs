namespace SocialNetwork.API.Helpers
{
    public class GroupParams : PaginationParams
    {
        public string? GroupName { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}
