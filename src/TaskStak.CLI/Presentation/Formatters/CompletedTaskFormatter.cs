using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class CompletedTaskFormatter(ITaskStakFormatter<DateTime> dateFormatter) : ITaskStakFormatter<TaskEntry>
    {
        public string Format(TaskEntry taskEntry)
        {
            var completedDate = taskEntry.Timeline.CompletedOn ?? throw new NullReferenceException("Completed date cannot be null.");

            return $"{taskEntry.Title} (completed {dateFormatter.Format(completedDate)})";
        }
    }
}
