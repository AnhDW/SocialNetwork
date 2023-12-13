namespace SocialNetwork.API.Entities
{
    public class Attachment : BaseEntity
    {
        public string Url { get; set; }
        public string FileType { get; set; }
        public int PostId {  get; set; }  

        public Post Post { get; set; }
    }
}