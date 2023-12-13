namespace SocialNetwork.API.Dtos
{
    public class UpdateProfileDto
    {
        public string KnownAs { get; set; }
        public string Bio { get; set; }
        public string Interests { get; set; }
        public string Location { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
