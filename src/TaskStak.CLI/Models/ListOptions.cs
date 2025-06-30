namespace TaskStak.CLI.Models
{
    public class ListOptions
    {
        public DateOnly? Date { get; set; }

        public bool Unstaged { get; set; }

        public bool Verbose { get; set; }
    }
}
