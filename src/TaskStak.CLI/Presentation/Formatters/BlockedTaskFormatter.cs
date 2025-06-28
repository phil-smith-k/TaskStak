using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class BlockedTaskFormatter : TaskFormatterBase
    {
        public override string Format(TaskEntry task)
        {
            var symbol = Constants.DisplaySymbol.Blocked;
            var title = task.Title;

            var formattedDate = AgoFormatter.Format(task.Timeline.CreatedOn);

            return FormatWithAlignment(symbol, title, formattedDate);
        }
    }
}
