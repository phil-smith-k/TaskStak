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
            var viewArg = new Argument<ViewArgument>(
                name: Constants.Arguments.View, 
                isDefault: true,
                parse: ParseViewArgument,
                description: Constants.Arguments.Descriptions.ViewDesc);

            var command = new Command(Name, Description)
            {
                viewArg,
            };

            command.AddAlias(Constants.Commands.ViewAlias);
            command.SetHandler(Execute, viewArg);

            return command;
        }

        public static void Execute(ViewArgument viewArg)
        {
            var tasks = JsonHelper.LoadTasks();
            var options = new ListOptions
            {
                ViewArgument = viewArg,
            };

            if (tasks.Count == 0)
            {
                Console.WriteLine(Constants.Messages.NoTasksFound);
                return;
            }

            var view = ListViewFactory.GetViewFor(options);
            view.Render(tasks);
        }

        private static ViewArgument ParseViewArgument(ArgumentResult argResult)
        {
            ViewArgument result = default;
            var arg = argResult.Tokens.SingleOrDefault()?.Value;

            if (string.IsNullOrWhiteSpace(arg))
                return result;

            var parsed = Enum.TryParse(arg, ignoreCase: true, out result);
            if (parsed)
                return result;

            result = arg.ToLowerInvariant() switch
            {
                "d" => ViewArgument.Day,
                "w" => ViewArgument.Week,

                _ => throw new ArgumentException($"Invalid view argument '{arg}'. Run task --help to see view argument options."),
            };

            return result;
        }
    }
}
