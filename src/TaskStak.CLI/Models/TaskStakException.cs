namespace TaskStak.CLI.Models
{
    public class TaskStakException : Exception
    {
        public TaskStakException(string message) : base(message) { }
        public TaskStakException(string message, Exception innerException) : base(message, innerException) { }
    }
}
