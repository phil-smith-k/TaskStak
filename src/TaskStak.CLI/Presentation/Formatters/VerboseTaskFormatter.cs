using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class VerboseTaskFormatter : TaskFormatterBase
    {
        private ITaskStakFormatter<DateTimeOffset> DateTimeVerbose => new DateTimeVerboseFormatter();

        public override string Format(TaskEntry item)
        {
            var symbol = GetSymbol(item.Status.Value);
            var formattedDate = DateTimeVerbose.Format(item.Timeline.CreatedOn);

            return FormatWithAlignment(symbol, item.Id, item.Title, formattedDate);
        }

        private static string GetSymbol(TaskEntryStatus status)
        {
            return status switch
            {
                TaskEntryStatus.Active => Constants.DisplaySymbol.Active,
                TaskEntryStatus.Blocked => Constants.DisplaySymbol.Blocked,
                TaskEntryStatus.Completed => Constants.DisplaySymbol.Complete,

                _ => throw new ArgumentOutOfRangeException($"Invalid enum value {(int)status} for {typeof(TaskEntryStatus)}"),
            };
        }
    }
}
