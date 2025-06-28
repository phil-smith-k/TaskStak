namespace TaskStak.CLI.Presentation.Formatters
{
    internal class HeaderFormatter : FormatterBase<string>
    {
        public override string Format(string item)
        {
            var maxWidth = GetMaxWidth();
            return $"{FormatWithRightAlignment(item)}\n{FormatPadding('-', maxWidth)}\n";
        }
    }
}
