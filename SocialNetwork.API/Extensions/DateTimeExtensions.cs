namespace SocialNetwork.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CacuateAge(this DateTime dob)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }
}
