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
            CreateMap<User, OwnerDto>();
            CreateMap<UpdateProfileDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>()
                .ForMember(dest=>dest.Countdown,
                opt=>opt.MapFrom(src=>src.Timestamp.CountdownTime()));
            CreateMap<Post, PostDto>()
                .ForMember(dest=>dest.Countdown,
                opt=>opt.MapFrom(src=>src.CreatedDate.CountdownTime()));
            CreateMap<LikePost, LikePostDto>();
            CreateMap<LikePostDto, LikePost>();
            CreateMap<Chat, ChatDto>();
            CreateMap<PostDto, Post>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<GroupMember, GroupMemberDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomChat, RoomChatDto>();
            CreateMap<TokenManagementDto, TokenManagement>();
            CreateMap<TokenManagement, TokenManagementDto>()
                .ForMember(dest => dest.DateOfExistence, 
                opt => opt.MapFrom(src => src.Created.CacuateTime()));
        }
    }
}
