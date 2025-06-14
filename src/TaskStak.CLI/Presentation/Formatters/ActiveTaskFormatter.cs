using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class ActiveTaskFormatter : ITaskStakFormatter<TaskEntry>
    {
        private static ITaskStakFormatter<DateTime> AgoFormatter => new DateTimeAgoFormatter();

        public string Format(TaskEntry taskEntry)
            => $"{Constants.Emojis.Star} {taskEntry.Title} ({AgoFormatter.Format(taskEntry.Timeline.CreatedOn)})";
    }
}
