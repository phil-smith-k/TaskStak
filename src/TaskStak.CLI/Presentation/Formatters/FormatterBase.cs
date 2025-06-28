namespace TaskStak.CLI.Presentation.Formatters
{
    public abstract class FormatterBase<T> : ITaskStakFormatter<T>
    {
        protected const int MAX_FORMATTED_WIDTH = 80;

        public abstract string Format(T item);

        protected static string FormatPadding(char character, int padding)
             => new(character, Math.Max(1, padding));

        protected static string FormatWithLeftAlignment(string str, char paddingChar = ' ')
        {
            var maxWidth = GetMaxWidth();
            var padding = maxWidth - str.Length - 1;

            return $"{str} {FormatPadding(paddingChar, padding)}";
        }

        protected static string FormatWithRightAlignment(string str, char paddingChar = ' ')
        {
            var maxWidth = GetMaxWidth();
            var padding = maxWidth - str.Length - 1;

            return $"{FormatPadding(paddingChar, padding)} {str}";
        }

        protected static int GetMaxWidth()
            => Math.Min(GetSafeTerminalWidth(), MAX_FORMATTED_WIDTH);

        protected static int GetSafeTerminalWidth()
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
    }
}
