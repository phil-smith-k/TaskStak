using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class CompletedTaskFormatter : TaskFormatterBase
    {
        public override string Format(TaskEntry taskEntry)
        {
            var emoji = Constants.DisplaySymbol.Complete;
            var title = taskEntry.Title;

            var completedDate = taskEntry.Timeline.CompletedOn ?? throw new NullReferenceException("Completed date cannot be null.");
            var formattedDate = AgoFormatter.Format(completedDate);

            return FormatWithAlignment(emoji, title, formattedDate);
        }
    }
}
