namespace SocialNetwork.API.Entities
{
    public class Room : BaseEntity
    {
        public string RoomName { get; set; }
        public string? Description { get; set;}
        public int CreatorId { get; set; }

        public User User { get; set; }
        public List<RoomMember> RoomMembers { get; set; }
        public List<ChatRoom> ChatRooms { get; set; }
    }
}
