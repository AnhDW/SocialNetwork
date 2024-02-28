namespace SocialNetwork.API.Entities
{
    public class GroupMember
    {
        public int MemberId {  get; set; }
        public int GroupId { get; set; }
        public string Role { get; set; } = "member";
        public DateTime JoinAt { get; set; } = DateTime.UtcNow;

        public Group Group { get; set; }
        public User Member { get; set; }
    }
}
