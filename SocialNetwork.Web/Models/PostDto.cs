namespace SocialNetwork.Web.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Content { get; set; }
        public string ShowMode { get; set; } = "Global"; //Global, Friends, Personal, SpecifiedObject
        public string Countdown { get; set; }
        public OwnerDto User { get; set; }
        public List<AttachmentDto> Attachments { get; set; } = new();
        //public List<CommentDto> Comments { get; set; } = new();

    }
}
