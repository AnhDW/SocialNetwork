using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using SocialNetwork.Web.Helpers;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;
using System.Diagnostics;

namespace SocialNetwork.Web.Controllers
{
    [ServiceFilter(typeof(SetLayoutViewBagFilter))]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenProvider _tokenProvider;

        public HomeController(IUserService userService, ITokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_tokenProvider.GetToken()!=null)
            {
                var user = _tokenProvider.GetCookies("_current_user");
                //ViewBag.User = user;
                return View();
            }
            return RedirectToAction("Index", "Account", new { area = "" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
