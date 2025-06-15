using TaskStak.CLI.Models;

namespace TaskStak.CLI.Services
{
    public interface ISearchService<T> where T : EntityRoot
    {
        SearchResult<T> Search(IEnumerable<T> entities, ISearchCriteria<T> criteria);
    }
}
