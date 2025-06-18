namespace TaskStak.CLI.Utils
{
    public static class DateTimeExtensions
    {
        public static bool IsToday(this DateTime? src)
            => src?.Date == DateTime.Now.Date;

        public static bool IsToday(this DateTime src)
            => src.Date == DateTime.Now.Date;
    }
}
