namespace SocialNetwork.API.Dtos
{
    public class InteractWithPostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public int Age { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
