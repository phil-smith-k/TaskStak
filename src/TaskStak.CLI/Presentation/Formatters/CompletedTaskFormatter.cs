using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class CompletedTaskFormatter : TaskFormatterBase
    {
        private readonly ITaskStakFormatter<DateTime> _verboseFormatter = new DateTimeVerboseFormatter();

        public override string Format(TaskEntry taskEntry)
        {
            var symbol = Constants.DisplaySymbol.Complete;
            var title = taskEntry.Title;

            var completedDate = taskEntry.Timeline.CompletedOn ?? throw new NullReferenceException("Completed date cannot be null.");

            var formattedDate = taskEntry.Timeline.CompletedOn.IsToday()
                ? AgoFormatter.Format(completedDate)
                : _verboseFormatter.Format(completedDate);

            return FormatWithAlignment(symbol, title, formattedDate);
        }
    }
}
