using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;

namespace SocialNetwork.Web.Components
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public CommentsViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            ResponseDto? response = await _commentService.GetComments(postId);
            if(response != null && response.IsSuccess)
            {
                List<CommentDto>? comments = JsonConvert.DeserializeObject<List<CommentDto>>(Convert.ToString(response.Result)!);
                return View(comments);
            }
            return View();
        }
    }
}
