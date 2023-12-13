namespace SocialNetwork.API.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public string NotificationType { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
    }
}
