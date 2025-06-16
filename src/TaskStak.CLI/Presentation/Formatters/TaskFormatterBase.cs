using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Formatters
{
    public abstract class TaskFormatterBase : ITaskStakFormatter<TaskEntry>
    {
        private const int MAX_FORMATTED_WIDTH = 80;

        protected ITaskStakFormatter<DateTime> AgoFormatter => new DateTimeAgoFormatter();

        public abstract string Format(TaskEntry item);

        protected static string FormatWithAlignment(string symbol, string id, string title, string formattedDate)
        {
            var maxWidth = Math.Min(GetSafeTerminalWidth(), MAX_FORMATTED_WIDTH);

            var maxTitleLength = maxWidth 
                              - symbol.Length 
                              - id.Length
                              - formattedDate.Length 
                              - 1  // 1 space before id 
                              - 1  // 1 space before title
                              - 1; // 1 space before date

            if (title.Length > maxTitleLength)
            {
                title = TruncateTitle(title, maxTitleLength);
            }

            var padding = maxWidth - symbol.Length - id.Length - title.Length - formattedDate.Length - 2; // 2 for spaces before id and title
            return $"{symbol} {id} {title}{FormatPadding(padding)}{formattedDate}";
        }

        protected static string FormatWithAlignment(string symbol, string title, string formattedDate)
        {
            var maxWidth = Math.Min(GetSafeTerminalWidth(), MAX_FORMATTED_WIDTH);

            var maxTitleLength = maxWidth 
                                 - symbol.Length 
                                 - formattedDate.Length 
                                 - 1  // 1 space before title 
                                 - 1; // 1 space before date

            if (title.Length > maxTitleLength)
            {
                title = TruncateTitle(title, maxTitleLength);
            }

            var padding = maxWidth - symbol.Length - title.Length - formattedDate.Length - 1; // 1 for space before title
            return $"{symbol} {title}{FormatPadding(padding)}{formattedDate}";
        }

        private static int GetSafeTerminalWidth()
        {
            try
            {
                return Console.WindowWidth;
            }
            catch
            {
                return MAX_FORMATTED_WIDTH; 
            }
        }

        private static string TruncateTitle(string title, int availableSpace)
        {
            const string ellipsis = "...";

            var maxLength = availableSpace - ellipsis.Length;
            return maxLength > 0
                ? title[..maxLength] + ellipsis
                : ellipsis; 
        }

        private static string FormatPadding(int padding)
            => new string(' ', Math.Max(1, padding));
    }
}
