namespace TaskStak.CLI.Utils
{
    public static class DateTimeExtensions
    {
        public static bool IsToday(this DateTime? src)
            => src?.Date == DateTime.Now.Date;

        public static bool IsToday(this DateTime src)
            => src.Date == DateTime.Now.Date;

        public static bool IsYesterday(this DateTime? src)
            => src?.Date == DateTime.Now.Date.AddDays(-1);

        public static bool IsYesterday(this DateTime src)
            => src.Date == DateTime.Now.Date.AddDays(-1);
    }
}
