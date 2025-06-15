using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
using TaskStak.CLI.Services;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class DoneCommand : ITaskStakCommand
    {
        private static readonly ISearchService<TaskEntry> _searchService = new TaskSearchService();

        public static string Name => Constants.Commands.Done;
        public static string Description => Constants.Commands.Descriptions.DoneDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string[]>(Constants.Arguments.Query, Constants.Arguments.Descriptions.QueryDesc)
            {
                Arity = ArgumentArity.OneOrMore
            };

            var command = new Command(Name, Description)
            {
                queryArg,
            };

            command.SetHandler(Execute, queryArg);

            return command;
        }

        public static void Execute(string[] queryArg)
        {
            var tasks = JsonHelper.LoadTasks();
            var results = _searchService.Search(tasks, new TaskSearchCriteria
            {
                Query = string.Join(" ", queryArg),
                StatusFlags = TaskEntryStatus.Active | TaskEntryStatus.Blocked,
            });

            if (results.EntityFound)
            {
                var task = results.GetEntity();

                task.Complete();
                JsonHelper.SaveTasks(tasks);

                Console.WriteLine(Constants.Messages.TaskCompleted, task.Title);
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
