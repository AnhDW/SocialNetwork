namespace SocialNetwork.API.Helpers
{
    public class RoomParams : PaginationParams
    {
        public int UserId {  get; set; }
        public int RoomId {  get; set; }
        public string Predicate { get; set; }
        public string OrderBy { get; set; } = "lastActive";
    }
}
