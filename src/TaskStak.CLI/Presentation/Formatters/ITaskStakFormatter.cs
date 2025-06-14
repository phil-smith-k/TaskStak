namespace TaskStak.CLI.Presentation.Formatters
{
    public interface ITaskStakFormatter<in T> 
    {
        string Format(T item);
    }
}
