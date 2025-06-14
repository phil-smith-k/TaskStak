using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class DoneCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Done;
        public static string Description => Constants.Commands.Descriptions.DoneDesc;

        public static Command Create()
        {
            var titleArg = new Argument<string[]>(Constants.Arguments.Title, Constants.Arguments.Descriptions.TitleDesc)
            {
                Arity = ArgumentArity.OneOrMore
            };

            var command = new Command(Name, Description)
            {
                titleArg,
            };

            command.SetHandler(Execute, titleArg);

            return command;
        }

        public static void Execute(string[] titleArgs)
        {
            var tasks = JsonHelper.LoadTasks();
            var filtered = tasks.Where(FilterPredicate).ToList();

            var count = filtered.Count;
            if (count == 0)
            {
                Console.WriteLine(Constants.Messages.TaskNotFound);
                return;
            }

            if (count > 1)
            {
                Console.WriteLine(Constants.Messages.MultipleTasksFound);
                return;
            }

            var task = tasks.First(t => t.Id == filtered.First().Id);

            task.Complete();
            JsonHelper.SaveTasks(tasks);

            Console.WriteLine(Constants.Messages.TaskCompleted, task.Title);
            return;

            bool FilterPredicate(TaskEntry tsk) => tsk.IsActive && 
                                                    titleArgs.All(arg => tsk.Title.Contains(arg, StringComparison.OrdinalIgnoreCase));
        }
    }
}
