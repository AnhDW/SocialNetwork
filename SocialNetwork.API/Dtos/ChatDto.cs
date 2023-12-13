namespace SocialNetwork.API.Dtos
{
    public class ChatDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderAvatarUrl { get; set; }
        
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAvatarUrl { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DateRead { get; set; }


    }
}
