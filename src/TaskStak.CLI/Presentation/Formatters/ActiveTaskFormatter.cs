using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Formatters
{
    public class ActiveTaskFormatter : TaskFormatterBase
    {
        protected ITaskStakFormatter<DateTime> CreatedOnFormatter => new CreatedOnFormatter();

        public override string Format(TaskEntry taskEntry)
        {
            var emoji = Constants.DisplaySymbol.Active;
            var title = taskEntry.Title;
            var formattedDate = CreatedOnFormatter.Format(taskEntry.Timeline.CreatedOn);

            return FormatWithAlignment(emoji, title, formattedDate);
        }
    }
}
