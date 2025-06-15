using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
using TaskStak.CLI.Services;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class TitleCommand : ITaskStakCommand
    {
        private static readonly ISearchService<TaskEntry> _searchService = new TaskSearchService();

        public static string Name => Constants.Commands.Title;
        public static string Description => Constants.Commands.Descriptions.TitleDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string>(Constants.Arguments.Query, Constants.Arguments.Descriptions.QueryDesc);
            var titleArg = new Argument<string>(Constants.Arguments.Title, Constants.Arguments.Descriptions.TitleDesc);

            var command = new Command(Name, Description)
            {
                queryArg,
                titleArg,
            };

            command.SetHandler(Execute, queryArg, titleArg);

            return command;
        }

        public static void Execute(string queryArg, string titleArg)
        {
            var tasks = JsonHelper.LoadTasks();
            var results = _searchService.Search(tasks, new TaskSearchCriteria
            {
                Query = queryArg,
                StatusFlags = TaskEntryStatus.All,
            });

            if (results.EntityFound)
            {
                var task = results.GetEntity();
                var original = task.Title;

                task.Title = titleArg;
                JsonHelper.SaveTasks(tasks);

                Console.WriteLine(Constants.Messages.TitleUpdated, original, titleArg);
            }
            else if (results.CandidatesFound)
            {
                Console.WriteLine(Constants.Messages.MultipleTasksFound);

                var view = ListViewFactory.GetViewFor(ViewOption.Verbose);
                view.Render(results.Candidates);
            }
            else if (results.NoResults)
            {
                Console.WriteLine(Constants.Messages.NoTasksFound);
            }
        }
    }
}
