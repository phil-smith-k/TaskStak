using System.CommandLine;
using System.CommandLine.Parsing;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    internal class EditCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Edit;
        public static string Description => Constants.Commands.Descriptions.EditDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string>(Constants.Arguments.Query, Constants.Arguments.Descriptions.QueryDesc);

            var statusOption = new Option<TaskEntryStatus?>(
                aliases: [Constants.Options.Status, Constants.Options.StatusAlias],
                parseArgument: ParseStatusArgument,
                isDefault: true,
                Constants.Options.Descriptions.StatusDesc);

            var titleOption = new Option<string>(
                aliases: [Constants.Options.Title, Constants.Options.TitleAlias], 
                description: Constants.Arguments.Descriptions.TitleDesc);

            var command = new Command(Name, Description)
            {
                queryArg,
                statusOption,
                titleOption,
            };

            command.SetHandler(Execute, queryArg, statusOption, titleOption);
            return command;
        }

        private static void Execute(string queryArg, TaskEntryStatus? statusOption, string titleOption)
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
                    var isUpdated = task.Edit(statusOption, titleOption);
                    JsonHelper.SaveTasks(tasks);

                    if (isUpdated)
                        Console.WriteLine(Constants.Messages.TaskUpdated, task.Title);
                });

            command.Execute();
        }

        private static TaskEntryStatus? ParseStatusArgument(ArgumentResult argResult)
        {
            var result = TaskEntryStatus.Active;
            var arg = argResult.Tokens.SingleOrDefault()?.Value;

            if (string.IsNullOrWhiteSpace(arg))
                return null;

            var parsed = Enum.TryParse(arg, ignoreCase: true, out result);
            if (parsed)
                return result;

            result = arg.ToLowerInvariant() switch
            {
                "a" => TaskEntryStatus.Active,
                "b" => TaskEntryStatus.Blocked,
                "c" => TaskEntryStatus.Completed,

                _ => throw new TaskStakException($"Invalid status '{arg}'. Valid options: active (a), blocked (b), completed (c)"),
            };

            return result;
        }

        private static void ValidateDateArg(ArgumentResult result)
        {
            var date = result.GetValueForArgument(result.Argument) as DateOnly?;

            if (date == DateOnly.MinValue) // Sentinel value indicates argument was supplied, but parsing failed
            {
                result.ErrorMessage = "Invalid date format. Use any standard date format or use --today, --tomorrow, --monday, --tuesday, etc.";
            }
        }
    }
}
