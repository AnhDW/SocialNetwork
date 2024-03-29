﻿namespace SocialNetwork.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string AvatarUrl{ get; set; }
        public string KnownAs { get; set; }
        public string Gender { get; set; }
    }
}