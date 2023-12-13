namespace SocialNetwork.API.Helpers
{
    public class FriendsParams : PaginationParams
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public string Predicate { get; set; }
    }
}
