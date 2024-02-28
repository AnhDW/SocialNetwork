namespace SocialNetwork.API.Dtos
{
    public class InteractWithRoomDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int MemberId { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public int Age { get; set; }
        public DateTime JoinAt { get; set; }
    }
}
