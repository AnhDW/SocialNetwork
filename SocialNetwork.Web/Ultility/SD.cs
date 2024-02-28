namespace SocialNetwork.Web.Ultility
{
    public class SD
    {
        public static string SocialNetworkAPIBase { get; set; }
        public const string TokenCookie = "_token";
        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }

        public enum ReactionType
        {
            Like,
            Heart,
            Haha,
            Wow,
            Angry, 
            Cry,
        }
    }
}
