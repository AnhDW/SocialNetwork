using Microsoft.AspNetCore.Http;

namespace SocialNetwork.API.Services.IServices
{
    public interface IAttachmentSvc
    {
        string AddAttachment(IFormFile file);
        void DeleteAttachment(string url);
    }
}
