using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;

namespace SocialNetwork.API.Services.IServices
{
    public interface INotificationRepo
    {
        Task<PagedList<Notification>> GetNotifications();
        Task CreateNotification(Notification notification);
        Task SeenNotification(Notification notification);
    }
}
