using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class OverdueTaskFormatter : TaskFormatterBase
    {
        public override string Format(TaskEntry taskEntry)
        {
            if (!taskEntry.Timeline.StagedFor.HasValue || taskEntry.Timeline.StagedFor >= DateOnly.FromDateTime(DateTime.Today))
                   throw new ArgumentException($"Task must be staged for a past date to be used with {nameof(OverdueTaskFormatter)}", nameof(taskEntry));

            var symbol = Constants.DisplaySymbol.Active;
            var title = taskEntry.Title;

            var stagedFor = taskEntry.Timeline.StagedFor.Value;
            var formattedDate = $"from {AgoFormatter.Format(stagedFor.ToDateTime())}";

            return FormatWithAlignment(symbol, title, formattedDate);
        }
    }
}
