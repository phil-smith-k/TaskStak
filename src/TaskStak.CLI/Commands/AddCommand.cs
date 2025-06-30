using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class AddCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Add;
        public static string Description => Constants.Commands.Descriptions.AddDesc;

        public static Command Create()
        {
            var titleArg = new Argument<string>(Constants.Arguments.Title, Constants.Arguments.Descriptions.TitleDesc);

            var dateArg = new Argument<DateOnly?>(
                name: Constants.Arguments.Date,
                parse: ParseArgument.ParseOptionalDateArgument,
                isDefault: true,
                description: Constants.Arguments.Descriptions.DateDesc);

            dateArg.AddValidator(ValidateDateArg);

            var statusOption = new Option<TaskEntryStatus>(
                aliases: [Constants.Options.Status, Constants.Options.StatusAlias], 
                parseArgument: ParseStatusArgument,
                isDefault: true,
                Constants.Options.Descriptions.StatusDesc);

            var command = new Command(Name, Description)
            {
                titleArg,
                dateArg,
                statusOption,
            };

            command.SetHandler(Execute, titleArg, dateArg, statusOption);

            return command;
        }

        public static void Execute(string titleArg, DateOnly? dateArg, TaskEntryStatus status)
        {
            var tasks = JsonHelper.LoadTasks();

            var task = TaskEntry.New(titleArg, status);
            tasks.Add(task);

            if (dateArg.HasValue)
            {
                task.StageToDate(dateArg.Value);

                Console.WriteLine(Constants.Messages.TaskAddedOnDate, task.Title, dateArg?.ToString("ddd", CultureInfo.CurrentCulture), dateArg?.ToString("d", CultureInfo.CurrentCulture));
            }
            else
            {
                JsonHelper.SaveTasks(tasks);

                Console.WriteLine(Constants.Messages.TaskAdded, titleArg);
            }


        }

        private static TaskEntryStatus ParseStatusArgument(ArgumentResult argResult)
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
