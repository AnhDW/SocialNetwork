using AutoMapper;
using SocialNetwork.API.Dtos;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Entities;

namespace SocialNetwork.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<User, PersonalPageDto>()
                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.CacuateAge()));
            CreateMap<UpdateProfileDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Post, PostDto>();
            CreateMap<Chat, ChatDto>();
            CreateMap<PostDto, Post>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Group, GroupDto>();
        }
    }
}
