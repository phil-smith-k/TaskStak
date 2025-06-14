using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class ActiveTaskFormatter(ITaskStakFormatter<DateTime> dateFormatter) : ITaskStakFormatter<TaskEntry>
    {
        public string Format(TaskEntry taskEntry)
            => $"{Constants.Emojis.Star} {taskEntry.Title} ({dateFormatter.Format(taskEntry.Timeline.CreatedOn)})";
    }
}
