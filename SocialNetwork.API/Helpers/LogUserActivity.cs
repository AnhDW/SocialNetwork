﻿using Microsoft.AspNetCore.Mvc.Filters;
using SocialNetwork.API.Extensions;
using SocialNetwork.API.Services.IServices;

namespace SocialNetwork.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var username = resultContext.HttpContext.User.GetUsername();

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepo>();
            var user = await repo.GetByUsernameAsync(username);

            user.LastActive = DateTime.UtcNow;

            //var tokens = user.TokenManagements.Where(d => d.Created.CacuateTime() > 7).ToList();

            //foreach (var token in tokens)
            //{
            //    token.IsActive = false;
            //}

            await repo.SaveAllAsync();
        }
    }
}
