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

        public static int CacuateTime(this DateTime dateCreate)
        {
            var today = DateTime.UtcNow;
            var day = (int)(today - dateCreate).TotalDays;
            return day;
        }

        public static string CountdownTime(this DateTime time)
        {
            var now = DateTime.UtcNow;
            string countdown = (now.AddYears(-1) > time) ?
                            -(now.Year - time.Year) + " năm trước" :
                            (now.AddMonths(-1) > time)  ?
                            -(now.Month - time.Month) + " tháng trước" :
                            (now.Day - time.Day) >= 7 ?
                            ((now.Day - time.Day) / 7) + " tuần trước" :
                            (now - time).TotalDays >= 1 ?
                            ((int)(now - time).TotalDays) + " ngày trước" :
                            (now - time).TotalHours >= 1 ?
                            ((int)(now - time).TotalHours) + " giờ trước" :
                            (now - time).TotalMinutes >= 1 ?
                            ((int)(now - time).TotalMinutes) + " phút trước" :
                            (now - time).TotalSeconds >= 1 ?
                            ((int)(now - time).TotalSeconds) + " giây trước" :
                            0 + "giây trước";
            return countdown;
        }
    }
}
