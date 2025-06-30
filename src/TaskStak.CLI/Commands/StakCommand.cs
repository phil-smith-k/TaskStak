using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class StakCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Stak;
        public static string Description => Constants.Commands.Descriptions.StakDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string>(
                name: Constants.Arguments.Query, 
                description: Constants.Arguments.Descriptions.QueryDesc);

            var dateArg = new Argument<DateOnly>(
                name: Constants.Arguments.Date,
                parse: ParseArgument.ParseDateArgument,
                isDefault: true,
                description: Constants.Arguments.Descriptions.DateDesc);

            var inDayOption = new Option<int?>(
                name: Constants.Options.InDay, 
                description: Constants.Options.Descriptions.InDayDesc);

            dateArg.AddValidator(ValidateDateArg);
            inDayOption.AddValidator(ValidateInDayOption);

            var command = new Command(Name, Description)
            {
                queryArg,
                dateArg,
                inDayOption,
            };

            command.AddValidator(result => ValidateCommand(result, dateArg, inDayOption));

            command.SetHandler(Execute, queryArg, dateArg, inDayOption);
            return command;
        }

        private static void Execute(string queryArg, DateOnly date, int? inDay)
        {
            var command = new TaskSearchCommand();
            var criteria = new TaskSearchCriteria
            {
                Query = queryArg,
                StatusFlags = TaskEntryStatus.Active | TaskEntryStatus.Blocked,
            };

            var today = DateTime.Today;
            var dateStagedFor = inDay.HasValue
                ? DateOnly.FromDateTime(today.AddDays(inDay.Value))
                : date;

            command
                .WithCriteria(criteria)
                .OnTaskFound((tasks, task) =>
                {
                    task.StageToDate(dateStagedFor);

                    JsonHelper.SaveTasks(tasks);

                    Console.WriteLine(Constants.Messages.TaskAddedToStak, task.Title, dateStagedFor.ToString("ddd", CultureInfo.CurrentCulture), dateStagedFor.ToString("d", CultureInfo.CurrentCulture));
                })
                .OnNoResult(() =>
                {
                    Console.WriteLine(Constants.Messages.QueryNotFoundInStatus, queryArg, "active or blocked");
                });

            command.Execute();
        }

        private static void ValidateDateArg(ArgumentResult result)
        {
            if (result.GetValueForArgument(result.Argument) is not DateOnly date || date == DateOnly.MinValue) // Sentinel value indicates argument was supplied, but parsing failed
            {
                result.ErrorMessage = "Invalid date format. Use any standard date format or use --today, --tomorrow, --monday, --tuesday, etc.";
            }
            else if (date < DateOnly.FromDateTime(DateTime.Today))
            {
                result.ErrorMessage = "Cannot stage task for a past date.";
            }
        }

        private static void ValidateInDayOption(OptionResult result)
        {
            if (result.GetValueForOption(result.Option) is int inDay && inDay < 0)
            {
                result.ErrorMessage = "The value for option '--in' must be a non-negative integer.";
            }
        }

        private static void ValidateCommand(CommandResult result, Argument<DateOnly> dateArg, Option<int?> inDayOption)
        {
            var date = result.GetValueForArgument(dateArg);
            var inDay = result.GetValueForOption(inDayOption);

            // Only validate if both arguments have valid values
            if (date != DateOnly.MinValue && inDay.HasValue)
            {
                result.ErrorMessage = "Cannot specify both a date argument and the '--in' option. Please use only one.";
            }
        }
    }
}
