using System.CommandLine;
using System.CommandLine.Parsing;
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
            var titleArg = new Argument<string[]>(Constants.Arguments.Title, Constants.Arguments.Descriptions.TitleDesc)
            {
                Arity = ArgumentArity.OneOrMore
            };
            var statusOption = new Option<TaskEntryStatus>(
                aliases: [Constants.Options.Status, Constants.Options.StatusAlias], 
                parseArgument: ParseArgument,
                isDefault: true,
                Constants.Options.Descriptions.StatusDesc);

            var command = new Command(Name, Description)
            {
                titleArg,
                statusOption,
            };

            command.SetHandler(Execute, titleArg, statusOption);

            return command;
        }

        public static void Execute(string[] titleArgs, TaskEntryStatus status)
        {
            var title = string.Join(" ", titleArgs);
            var tasks = JsonHelper.LoadTasks();

            tasks.Add(new TaskEntry(title, status));
            JsonHelper.SaveTasks(tasks);

            Console.WriteLine(Constants.Messages.TaskAdded);
        }

        private static TaskEntryStatus ParseArgument(ArgumentResult argResult)
        {
            TaskEntryStatus result = TaskEntryStatus.Active;
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

                _ => throw new ArgumentException($"Invalid view argument '{arg}'. Run task --help to see view argument options."),
            };

            return result;
        }
    }
}
