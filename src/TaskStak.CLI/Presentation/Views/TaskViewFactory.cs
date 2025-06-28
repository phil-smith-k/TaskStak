using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Views
{
    public class TaskViewFactory
    {
        public static ITaskView GetViewFor(ListOptions options)
        {
            var date = options.Date;

            if (date == DateOnly.MinValue)
                throw new ArgumentException("Date cannot be default value", nameof(options.Date));

            if (date.IsToday())
            {
                return new TodayView(options);
            }
            else
            {
                return new DateView(options);
            }
        }

        public static ITaskView GetViewFor(DateOnly arg)
        {
            var options = new ListOptions { Date = arg };

            return GetViewFor(options);
        }
    }
}
