namespace SocialNetwork.API.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string? Description { get; set; }
        public int CreatorId { get; set; }
    }
}
