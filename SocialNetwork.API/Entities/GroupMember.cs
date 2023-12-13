namespace SocialNetwork.API.Entities
{
    public class GroupMember : BaseEntity
    {
        public int MemberId {  get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }
        public User Member { get; set; }
    }
}
