using System.CommandLine;

namespace TaskStak.CLI.Commands
{
    public class AddCommand : ITaskStakCommand
    {
        public static string Name => "add";
        public static string Description => "Add a new task";

        public static Command Create()
        {
            var titleArg = new Argument<string>(ArgumentName.Title);

            var command = new Command(Name, Description)
            {
                titleArg,
            };

            command.SetHandler(Execute, titleArg);

            return command;
        }

        public static void Execute(string title)
        {
            Console.WriteLine(title);
        }
    }
}
