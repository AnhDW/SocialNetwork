namespace SocialNetwork.Web.Service.IService
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
        void SetCookies(string key, object obj);
        object? GetCookies(string key);
        void ClearAllCookies();
    }
}
