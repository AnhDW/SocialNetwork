namespace SocialNetwork.API.Entities
{
    public class RoomMember
    {
        public int RoomId { get; set; }
        public int MemberId { get; set; }
        public string Role { get; set; } = "member";
        public DateTime JoinAt { get; set; } = DateTime.UtcNow;

        public Room Room { get; set; }
        public User Member { get; set; }
    }
}
