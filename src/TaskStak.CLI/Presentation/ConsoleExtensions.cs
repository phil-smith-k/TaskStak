namespace TaskStak.CLI.Presentation
{
    public static class ConsoleExtensions
    {
        public static void WriteLineColor(this TextWriter writer, string text, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}
