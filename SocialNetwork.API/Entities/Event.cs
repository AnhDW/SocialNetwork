namespace SocialNetwork.API.Entities
{
    public class Event : BaseEntity
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int CreatorId { get; set; }

        public User User { get; set; }
    }
}
