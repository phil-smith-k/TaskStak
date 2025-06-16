using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
using TaskStak.CLI.Services;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public delegate void TaskFoundHandler(List<TaskEntry> tasks, TaskEntry task);
    public delegate void CandidatesFoundHandler(List<TaskEntry> candidates);
    public delegate void NoResultsHandler();

    public class TaskSearchCommand
    {
        private readonly TaskSearchService _searchService = new();

        private TaskSearchCriteria? _criteria;

        private TaskFoundHandler _onTaskFound = (tasks, task) 
            => throw new NotImplementedException("On task found implementation is required.");

        private NoResultsHandler _onNoResults = () 
            => Console.WriteLine(Constants.Messages.NoTasksFound);

        private CandidatesFoundHandler _onCandidatesFound = (tasks) =>
        {
            Console.WriteLine(Constants.Messages.MultipleTasksFound);

            var view = ListViewFactory.GetViewFor(ViewOption.Verbose);
            view.Render(tasks);
        };

        public TaskSearchCommand WithCriteria(TaskSearchCriteria criteria)
        {
            this._criteria = criteria;
            return this;
        }

        public TaskSearchCommand OnTaskFound(TaskFoundHandler handler)
        {
            this._onTaskFound = handler;
            return this;
        }

        public TaskSearchCommand OnCandidatesFound(CandidatesFoundHandler handler)
        {
            this._onCandidatesFound = handler;
            return this;
        }

        public TaskSearchCommand OnNoResult(NoResultsHandler handler)
        {
            this._onNoResults = handler;
            return this;
        }

        public void Execute()
        {
            if (_criteria == null)
                throw new ArgumentNullException(nameof(_criteria), "Search criteria is required.");

            var tasks = JsonHelper.LoadTasks();
            var results = _searchService.Search(tasks, _criteria);

            if (results.EntityFound)
            {
                _onTaskFound(tasks, results.GetEntity());
            }
            else if (results.CandidatesFound)
            {
                _onCandidatesFound(results.Candidates);
            }
            else if (results.NoResults)
            {
                _onNoResults();
            }
        }
    }
}
