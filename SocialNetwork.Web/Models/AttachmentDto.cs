namespace SocialNetwork.Web.Models
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public required string FileType { get; set; }
    }
}
