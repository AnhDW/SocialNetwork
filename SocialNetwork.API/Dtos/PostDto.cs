using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ShowMode { get; set; } = "Global"; //Global, Friends, Personal, SpecifiedObject
        public List<AttachmentDto> Attachments { get; set; } = new();
    }
}
