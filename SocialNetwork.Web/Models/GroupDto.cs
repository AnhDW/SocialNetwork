namespace SocialNetwork.Web.Models
{
    public class GroupDto
    {
        public int Id { get; set; }
        public required string GroupName { get; set; }
        public required string Description { get; set; }
        public int CreatorId { get; set; }
    }
}
