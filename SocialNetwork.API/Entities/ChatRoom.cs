namespace SocialNetwork.API.Entities
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Room Room { get; set; }
        public User User { get; set; }
    }
}
