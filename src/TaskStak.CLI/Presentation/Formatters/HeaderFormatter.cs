namespace TaskStak.CLI.Presentation.Formatters
{
    internal class HeaderFormatter : FormatterBase<string>
    {
        public override string Format(string item)
        {
            return FormatWithRightAlignment(item, '-');
        }
    }
}
