namespace TaskStak.CLI.Persistence.Models
{
    public class Timeline
    {
        public DateTimeOffset? CompletedOn { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? LastModifiedOn { get; set; }

        public DateOnly? StagedFor { get; set; }
    }
}
