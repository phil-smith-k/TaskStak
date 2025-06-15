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
            var titleArg = new Argument<string[]>(Constants.Arguments.Title, Constants.Arguments.Descriptions.TitleDesc)
            {
                Arity = ArgumentArity.OneOrMore
            };

            var command = new Command(Name, Description)
            {
                titleArg,
            };

            command.SetHandler(Execute, titleArg);

            return command;
        }

        public static void Execute(string[] searchArgs)
        {
            var tasks = JsonHelper.LoadTasks();
            var results = _searchService.Search(tasks, new TaskSearchCriteria
            {
                Query = string.Join(" ", searchArgs),
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

                var view = ListViewFactory.GetViewFor(ViewArgument.Verbose);
                view.Render(results.Candidates);
            }
            else if (results.NoResults)
            {
                Console.WriteLine(Constants.Messages.NoTasksFound);
            }
        }
    }
}
