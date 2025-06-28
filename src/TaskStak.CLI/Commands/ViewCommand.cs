using System.CommandLine;
using System.CommandLine.Parsing;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class ViewCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.View;
        public static string Description => Constants.Commands.Descriptions.ViewDesc;

        public static Command Create()
        {
            var dateArg = new Argument<DateOnly>(
                name: Constants.Arguments.Date,
                parse: ParseArgument.ParseDateArgument,
                isDefault: true,
                description: Constants.Arguments.Descriptions.DateDesc);

            var verboseOption = new Option<bool>(
                aliases: [Constants.Options.Verbose, Constants.Options.VerboseAlias], 
                description: Constants.Options.Descriptions.VerboseDesc);

            dateArg.AddValidator(ValidateDateArg);

            var command = new Command(Name, Description)
            {
                dateArg,
                verboseOption,
            };

            command.SetHandler(Execute, dateArg, verboseOption);
            return command;
        }

        public static void Execute(DateOnly dateArg, bool verboseOption)
        {
            var tasks = JsonHelper.LoadTasks();
            var options = new ListOptions
            {
                Date = dateArg,
                Verbose = verboseOption, 
            };

            var view = TaskViewFactory.GetViewFor(options);
            view.RenderTasks(tasks);
        }

        private static void ValidateDateArg(ArgumentResult result)
        {
            if (result.GetValueForArgument(result.Argument) is not DateOnly date || date == DateOnly.MinValue)
            {
                result.ErrorMessage = "Invalid date format. Use any standard date format or use --today, --tomorrow, --monday, --tuesday, etc.";
            }
        }
    }
}
