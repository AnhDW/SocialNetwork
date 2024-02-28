using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;

namespace SocialNetwork.Web.Components
{
    public class LikeButtonsViewComponent : ViewComponent
    {
        private readonly ILikePostService _likePostService;

        public LikeButtonsViewComponent(ILikePostService likePostService)
        {
            _likePostService = likePostService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            ResponseDto? response = await _likePostService.IsLikePost(postId);
            if(response != null && response.IsSuccess)
            {
                LikePostButtonDto? isLike = JsonConvert.DeserializeObject<LikePostButtonDto>(Convert.ToString(response.Result)!);
                return View(isLike);
            }
            return View();
        }

        [HttpPost]
        public async Task<IViewComponentResult> AddLikePost(LikePostButtonDto likePostButtonDto)
        {
            return View();
        }
    }
}
