using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Service.IService
{
    public interface ICommentService
    {
        Task<ResponseDto?> GetComments(int postId);
    }
}
