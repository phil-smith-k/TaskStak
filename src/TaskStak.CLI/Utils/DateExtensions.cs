namespace TaskStak.CLI.Utils
{
    public static class DateExtensions
    {
        public static bool IsToday(this DateTime? src)
            => src?.Date == DateTime.Now.Date;

        public static bool IsToday(this DateTime src)
            => src.Date == DateTime.Now.Date;

        public static bool IsToday(this DateTimeOffset? src)
            => src?.Date == DateTimeOffset.Now.Date;

        public static bool IsToday(this DateTimeOffset src)
            => src.Date == DateTimeOffset.Now.Date;

        public static bool IsToday(this DateOnly? src)
            => src == DateOnly.FromDateTime(DateTime.Today);

        public static bool IsToday(this DateOnly src)
            => src == DateOnly.FromDateTime(DateTime.Today);

        public static DateTime ToDateTime(this DateOnly src, TimeOnly timeOnly = default)
            => src.ToDateTime(TimeOnly.MinValue);

        public static DateTime ToLocalTimeSpecifyKind(this DateTime src, DateTimeKind kind)
        {
            return DateTime.SpecifyKind(src, kind).ToLocalTime();
        }

        public static DateTime? ToLocalTimeSpecifyKind(this DateTime? src, DateTimeKind kind)
        {
            return src.HasValue 
                ? src.Value.ToLocalTimeSpecifyKind(kind)
                : null;
        }

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
