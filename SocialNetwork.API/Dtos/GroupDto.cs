using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
    }
}
