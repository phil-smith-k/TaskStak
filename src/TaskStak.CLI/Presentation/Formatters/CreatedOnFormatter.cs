using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class CreatedOnFormatter : ITaskStakFormatter<DateTime>
    {
        private readonly ITaskStakFormatter<DateTime> _agoFormatter = new DateTimeAgoFormatter();

        public string Format(DateTime source)
        {
            var result = _agoFormatter.Format(source);

            var daysAgo = (DateTime.UtcNow - source).TotalDays;
            var dayOfWeek = source.Date.DayOfWeek.ToString().ToLower();

            if (source.IsToday())
            {
                result = $"{source:hh:mm tt}"; 
            }
            else if (source.IsYesterday())
            {
                result = "yesterday";
            }
            else if (daysAgo <= 5)
            {
                result = $"{dayOfWeek}";
            }

            return result;
        }
    }
}
