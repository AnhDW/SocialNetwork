using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;

namespace SocialNetwork.Web.Service
{
    public class CommentService : ICommentService
    {
        private readonly IBaseService _baseService;

        public CommentService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetComments(int postId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                Url = SD.SocialNetworkAPIBase + "/api/comments?postId=" + postId
            });
        }
    }
}
