namespace SocialNetwork.API.Entities
{
    public class Friend
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public string Status { get; set; } = "Online";
        public bool IsFriend { get; set; } = false;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;//last interaction

        public User CurrentUser { get; set; }
        public User ConnectedUser { get; set; }
    }
}
