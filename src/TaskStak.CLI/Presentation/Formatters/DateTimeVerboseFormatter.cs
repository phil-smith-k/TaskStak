namespace TaskStak.CLI.Presentation.Formatters
{
    public class DateTimeVerboseFormatter : ITaskStakFormatter<DateTimeOffset>
    {
        public string Format(DateTimeOffset date)
            => date.ToLocalTime().ToString("ddd MM/dd/yyyy hh:mm tt");
    }
}
