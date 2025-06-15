namespace TaskStak.CLI.Models
{
    public interface ISearchCriteria<T> where T : EntityRoot
    {
        string Query { get; set; }
    }
}
