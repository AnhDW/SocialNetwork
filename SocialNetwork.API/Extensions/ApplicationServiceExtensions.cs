using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<ILikePostRepo, LikePostRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<IFriendRepo, FriendRepo>();
            services.AddScoped<IChatRepo, ChatRepo>();
            services.AddScoped<IGroupRepo, GroupRepo>();
            services.AddScoped<IGroupMemberRepo, GroupMemberRepo>();
            services.AddScoped<IRoomRepo, RoomRepo>();
            services.AddScoped<IRoomMemberRepo, RoomMemberRepo>();
            services.AddScoped<IRoomChatRepo, RoomChatRepo>();
            services.AddScoped<ITokenSvc, TokenSvc>();
            services.AddScoped<IAttachmentSvc, AttachmentSvc>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<LogUserActivity>();
            
            return services;
        }
    }
}
