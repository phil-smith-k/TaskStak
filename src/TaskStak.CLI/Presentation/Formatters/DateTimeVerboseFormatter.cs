namespace TaskStak.CLI.Presentation.Formatters
{
    public class DateTimeVerboseFormatter : ITaskStakFormatter<DateTime>
    {
        public string Format(DateTime date)
            => date.ToString("ddd MM/dd/yyyy hh:mm tt");
    }
}
