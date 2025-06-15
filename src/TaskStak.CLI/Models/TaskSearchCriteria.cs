namespace TaskStak.CLI.Models
{
    public class TaskSearchCriteria : ISearchCriteria<TaskEntry>
    {
        public required string Query { get; set; }

        public TaskEntryStatus StatusFlags { get; set; } = TaskEntryStatus.Active;
    }
}
