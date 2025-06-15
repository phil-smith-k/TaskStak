namespace TaskStak.CLI.Models
{
    [Flags]
    public enum TaskEntryStatus
    {                                                  // (Binary, Decimal)
        Active          = 1 << 0,                      // (001,          1)
        Blocked         = 1 << 1,                      // (010,          2)
        Completed       = 1 << 2,                      // (100,          4)
                                                                          
        All             = Active | Blocked | Completed // (111,          7)
    }
}
