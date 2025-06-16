using System.CommandLine;
using System.CommandLine.Parsing;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class MoveCommand : ITaskStakCommand
    {
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
            var command = new TaskSearchCommand();
            var criteria = new TaskSearchCriteria
            {
                Query = queryArg,
                StatusFlags = TaskEntryStatus.All,
            };

            command
                .WithCriteria(criteria)
                .OnTaskFound((tasks, task) =>
                {
                    var original = task.Status.Value;

                    task.EditStatus(status);
                    JsonHelper.SaveTasks(tasks);

                    Console.WriteLine(Constants.Messages.TaskUpdated, nameof(task.Status).ToLowerInvariant(), original, task.Status.Value);
                });

            command.Execute();
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
