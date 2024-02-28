using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;
using SocialNetwork.Web.Service.IService;

namespace SocialNetwork.Web.Helpers
{
    public class SetLayoutViewBagFilter : IActionFilter
    {
        private readonly ITokenProvider _tokenProvider;

        public SetLayoutViewBagFilter(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                var value = _tokenProvider.GetCookies("_current_user");
                controller.ViewBag.User = value;
            }
        }
    }
}
