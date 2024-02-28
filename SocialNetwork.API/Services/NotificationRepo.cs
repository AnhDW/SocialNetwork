using SocialNetwork.API.Entities;
using SocialNetwork.API.Helpers;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class NotificationRepo : INotificationRepo
    {
        public Task CreateNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<Notification>> GetNotifications()
        {
            throw new NotImplementedException();
        }

        public Task SeenNotification(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
