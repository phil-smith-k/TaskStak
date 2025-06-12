namespace TaskStak.CLI.Models
{
    [Flags]
    public enum TaskEntryStatus
    {
        Active          = 1 << 0,
        Impeded         = 1 << 1, 
        Completed       = 1 << 2,
    }
}
