using System.CommandLine;
using System.CommandLine.Parsing;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
using TaskStak.CLI.Services;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class MoveCommand : ITaskStakCommand
    {
        private static readonly ISearchService<TaskEntry> _searchService = new TaskSearchService();

        public static string Name => Constants.Commands.Move;
        public static string Description => Constants.Commands.Descriptions.MoveDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string>(Constants.Arguments.Query, Constants.Arguments.Descriptions.QueryDesc);
            var statusOption = new Option<TaskEntryStatus>(
                aliases: [Constants.Options.Status, Constants.Options.StatusAlias],
                parseArgument: ParseArgument,
                isDefault: true,
                Constants.Options.Descriptions.StatusDesc);

            var command = new Command(Name, Description)
            {
                queryArg,
                statusOption,
            };

            command.SetHandler(Execute, queryArg, statusOption);

            return command;
        }

        public static void Execute(string queryArg, TaskEntryStatus status)
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
                var original = task.Status.Value;

                task.EditStatus(status);
                JsonHelper.SaveTasks(tasks);

                Console.WriteLine(Constants.Messages.TaskUpdated, nameof(task.Status).ToLowerInvariant(), original, task.Status.Value);
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

        private static TaskEntryStatus ParseArgument(ArgumentResult argResult)
        {
            var result = TaskEntryStatus.Active;
            var arg = argResult.Tokens.SingleOrDefault()?.Value;

            if (string.IsNullOrWhiteSpace(arg))
                return result;

            var parsed = Enum.TryParse(arg, ignoreCase: true, out result);
            if (parsed)
                return result;

            result = arg.ToLowerInvariant() switch
            {
                "a" => TaskEntryStatus.Active,
                "b" => TaskEntryStatus.Blocked,
                "c" => TaskEntryStatus.Completed,

                _ => throw new ArgumentException($"Invalid status option '{arg}'. Run task --help to see status options."),
            };

            return result;
        }
    }
}
