using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;

namespace SocialNetwork.Web.Components
{
    public class PostsViewComponent : ViewComponent
    {
        private readonly IPostService _postService;

        public PostsViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? userId)
        {
            ResponseDto? response = await _postService.GetPostListAsync(userId);
            if(response != null && response.IsSuccess)
            {
                List<PostDto>? posts = JsonConvert.DeserializeObject<List<PostDto>>(Convert.ToString(response.Result)!);
                return View(posts);
            }
            
            return View();
        }

    }
}
