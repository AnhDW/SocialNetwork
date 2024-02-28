namespace SocialNetwork.API.Dtos
{
    public class TokenManagementDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Token { get; set; }
        //public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public int DateOfExistence { get; set; }
    }
}
