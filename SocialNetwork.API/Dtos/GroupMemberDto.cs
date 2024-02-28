namespace SocialNetwork.API.Dtos
{
    public class GroupMemberDto
    {
        public int MemberId { get; set; }
        public int GroupId { get; set; }
        public string Role { get; set; } = "member";
    }
}
