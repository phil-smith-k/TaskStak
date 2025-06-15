using TaskStak.CLI.Models;

namespace TaskStak.CLI.Services
{
    public class TaskSearchService : ISearchService<TaskEntry>
    {
        public SearchResult<TaskEntry> Search(IEnumerable<TaskEntry> tasks, ISearchCriteria<TaskEntry> criteria)
        {
            if (criteria is not TaskSearchCriteria taskCriteria)
                throw new ArgumentException("Invalid search criteria type.", nameof(criteria));

            var query = taskCriteria.Query;
            if (EntityId.TryParse(query, out var id))
            {
                var task = tasks.SingleOrDefault(t => t.Id == id);
                return new SearchResult<TaskEntry>
                {
                    Entity = task,
                };
            }

            var searchTerms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var matches = tasks.Where(FilterPredicate).ToList();

            return matches.Count switch
            {
                1 => new SearchResult<TaskEntry> { Entity = matches.First() },
                > 1 => new SearchResult<TaskEntry> { Candidates = matches },
                _ => new SearchResult<TaskEntry>(),
            };

            bool FilterPredicate(TaskEntry tsk)
                => tsk.Status.AnyOn(taskCriteria.StatusFlags) &&
                   searchTerms.All(term => tsk.Title.Contains(term, StringComparison.OrdinalIgnoreCase));
        }
    }
}
