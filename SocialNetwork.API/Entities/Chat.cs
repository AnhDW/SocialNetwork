namespace SocialNetwork.API.Entities
{
    public class Chat : BaseEntity
    {
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
        public DateTime? DateRead { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
