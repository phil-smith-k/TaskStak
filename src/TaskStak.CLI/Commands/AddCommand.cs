using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class AddCommand : ITaskStakCommand
    {
        public const string Name = TaskStakCommands.Add;
        public const string Description = Descriptions.TaskStakCommands.Add;

        public static Command Create()
        {
            var titleArg = new Argument<string[]>(Arguments.Title, Descriptions.Arguments.Title)
            {
                Arity = ArgumentArity.OneOrMore
            };
            var statusOption = new Option<string>([Options.Status, Options.StatusAlias], Descriptions.Options.Status);

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
            Console.WriteLine(Messages.TaskAdded);
        }
    }
}
