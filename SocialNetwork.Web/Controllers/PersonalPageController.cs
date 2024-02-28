using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Web.Helpers;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;

namespace SocialNetwork.Web.Controllers
{
    [ServiceFilter(typeof(SetLayoutViewBagFilter))]
    public class PersonalPageController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILikePostService _likePostService;

        public PersonalPageController(IUserService userService, ITokenProvider tokenProvider, ILikePostService likePostService)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
            _likePostService = likePostService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if(_tokenProvider.GetToken()!=null)
            {
                ResponseDto? response = await _userService.GetTimeline(id);
                if (response!=null && response.IsSuccess)
                {
                    PersonalPageDto? personalPage = JsonConvert.DeserializeObject<PersonalPageDto>(Convert.ToString(response.Result)!);
                    return View(personalPage);
                }
            }

            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            _tokenProvider.ClearAllCookies();
            return RedirectToAction("Index", "Account");
        }
    }
}
