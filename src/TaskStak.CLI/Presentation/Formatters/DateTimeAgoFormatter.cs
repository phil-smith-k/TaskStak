namespace TaskStak.CLI.Presentation.Formatters
{
    public class DateTimeAgoFormatter : ITaskStakFormatter<DateTime>
    {
        public string Format(DateTime date)
        {
            var timeSpan = DateTime.UtcNow - date;

            if (timeSpan.TotalMinutes < 1)
            {
                return "just now";
            }

            if (timeSpan.TotalMinutes < 60)
            {
                return $"{(int)timeSpan.TotalMinutes}m ago";
            }

            if (timeSpan.TotalHours < 24)
            {
                return $"{(int)timeSpan.TotalHours}h ago";
            }

            if (timeSpan.TotalDays < 30)
            {
                return $"{(int)timeSpan.TotalDays}d ago";
            }

            if (timeSpan.TotalDays < 365)
            {
                return $"{(int)(timeSpan.TotalDays / 30)}mo ago";
            }

            return $"{(int)(timeSpan.TotalDays / 365)}yrs ago";
        }
    }
}
