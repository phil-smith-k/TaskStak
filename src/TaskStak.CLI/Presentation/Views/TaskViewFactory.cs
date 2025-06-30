using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Views
{
    public class TaskViewFactory
    {
        public static ITaskView GetViewFor(ListOptions options)
        {
            if (options.Unstaged)
            {
                return new UnstagedView(options);
            }

            if (options.Date.HasValue)
            {
                return options.Date.IsToday() 
                    ? new TodayView(options) 
                    : new DateView(options);
            }

            throw new ArgumentException("Invalid options provided for task view creation.", nameof(options));
        }

        public static ITaskView GetViewFor(DateOnly arg)
        {
            var options = new ListOptions { Date = arg };

            return GetViewFor(options);
        }
    }
}
