using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Services
{
    public class AttachmentSvc : IAttachmentSvc
    {
        public string AddAttachment(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fileType = file.ContentType.Split('/')[0] + "s";

            if (file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", @fileType, fileName);
                if(!Directory.Exists("wwwroot/" + fileType))
                {
                    Directory.CreateDirectory("wwwroot/" + fileType);
                }
                using (var stream = File.Create(path))
                {
                    file.CopyTo(stream);
                }

                return "/" + fileType + "/" + fileName;
            }
            
            return null;
        }

        public void DeleteAttachment(string url)
        {
            File.Delete("wwwroot" + url);
        }
    }
}
