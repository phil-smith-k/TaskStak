namespace TaskStak.CLI.Utils
{
    public static class DateTimeExtensions
    {
        public static bool IsToday(this DateTime? src)
            => src?.Date == DateTime.Now.Date;

        public static bool IsToday(this DateTime src)
            => src.Date == DateTime.Now.Date;

        public static bool IsToday(this DateOnly? src)
            => src == DateOnly.FromDateTime(DateTime.Today);

        public static bool IsToday(this DateOnly src)
            => src == DateOnly.FromDateTime(DateTime.Today);

        public static DateTime GetNextDayOfWeek(this DateTime src, DayOfWeek dayOfWeek)
        {
            var today = DateTime.Today;

            return src.AddDays(
                (int)dayOfWeek - (int)today.DayOfWeek > 0
                    ? (int)dayOfWeek - (int)today.DayOfWeek
                    : 7 + (int)dayOfWeek - (int)today.DayOfWeek);
        }
    }
}
