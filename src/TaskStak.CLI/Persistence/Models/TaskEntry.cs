namespace TaskStak.CLI.Persistence.Models
{
    public class TaskEntry
    {
        public required string Id { get; set; }

        public int Status { get; set; }

        public string? Title { get; set; }

        public Timeline? Timeline { get; set; }
    }
}
