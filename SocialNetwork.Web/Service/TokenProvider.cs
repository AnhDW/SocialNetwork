using Newtonsoft.Json;
using SocialNetwork.Web.Service.IService;
using SocialNetwork.Web.Ultility;

namespace SocialNetwork.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }

        public void SetCookies(string key, object obj)
        {
            var value = JsonConvert.SerializeObject(obj);
            _contextAccessor.HttpContext?.Response.Cookies.Append(key, value);
        }

        public object? GetCookies(string key)
        {
            string? obj = null;
            bool? hasObject = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(key, out obj);
            return hasObject is true ? JsonConvert.DeserializeObject(obj!) : null;
        }

        public void ClearAllCookies()
        {
            var cookieKeys = _contextAccessor.HttpContext?.Request.Cookies.Keys;
            foreach(var cookieKey in cookieKeys!)
            {
                _contextAccessor.HttpContext?.Response.Cookies.Delete(cookieKey);
            }
        }
    }
}
