namespace SocialNetwork.API.Entities
{
    public class Group : BaseEntity
    {
        public string GroupName {  get; set; }
        public string Description {  get; set; }
        public int CreatorId { get; set; }

        public User User { get; set; }
        public List<GroupMember> GroupMembers { get; set; }

    }
}
