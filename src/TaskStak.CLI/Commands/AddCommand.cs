using System.CommandLine;
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
            var statusOption = new Option<string>([Constants.Options.Status, Constants.Options.StatusAlias], Constants.Options.Descriptions.StatusDesc);

            var command = new Command(Name, Description)
            {
                titleArg,
                statusOption,
            };

            command.SetHandler(Execute, titleArg, statusOption);

            return command;
        }

        public static void Execute(string[] titleArgs, string statusOpt)
        {
            var title = string.Join(" ", titleArgs);
            var tasks = JsonHelper.LoadTasks();

            tasks.Add(new TaskEntry(title));
            JsonHelper.SaveTasks(tasks);

            Console.WriteLine(Constants.Messages.TaskAdded);
        }
    }
}
