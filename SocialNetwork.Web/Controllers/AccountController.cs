using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITokenProvider _tokenProvider;

        public AccountController(IAccountService accountService, ITokenProvider tokenProvider)
        {
            _accountService = accountService;
            _tokenProvider = tokenProvider;
        }
        public IActionResult Index()
        {
            if (_tokenProvider.GetToken()!=null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(BigViewModel bigViewModel)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _accountService.Login(bigViewModel.Login!);
                if (response != null && response.IsSuccess)
                {
                    UserDto? user = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result)!);

                    _tokenProvider.SetToken(user!.Token);
                    _tokenProvider.SetCookies("_current_user", user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(BigViewModel bigViewModel)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _accountService.Register(bigViewModel.Register!);
                if (response != null && response.IsSuccess)
                {
                    UserDto? user = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result)!);

                    _tokenProvider.SetToken(user!.Token);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return RedirectToAction("Index");
        }

        
    }
}
