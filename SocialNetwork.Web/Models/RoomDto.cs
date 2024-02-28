﻿namespace SocialNetwork.Web.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public required string RoomName { get; set; }
        public string? Description { get; set; }
        public int CreatorId { get; set; }
    }
}