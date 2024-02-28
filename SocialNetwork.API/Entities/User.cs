namespace SocialNetwork.API.Entities
{
    public class User : BaseEntity
    {
        public required string UserName { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public required string KnownAs { get; set; }
        public string? AvatarUrl { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
        public string? Interests { get; set; }
        public string? Location { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Friend> CurrentUsers { get; set; } = new();
        public List<Friend> ConnectedUsers { get; set; } = new();

        public List<Chat> SenderList { get; set; } = new();
        public List<Chat> RecipientList { get; set; } = new();

        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<LikePost> LikePosts { get; set; } = new();
        public List<Group> Groups { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
        public List<Room> Rooms { get; set; } = new();
        public List<GroupMember> GroupMembers { get; set; } = new();
        public List<RoomMember> RoomMembers { get; set; } = new();
        public List<RoomChat> ChatRooms { get; set; } = new();
        public List<TokenManagement> TokenManagements { get; set; } = new();
    }
}
