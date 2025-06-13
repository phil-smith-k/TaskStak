using System.CommandLine;
using System.Xml.Linq;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class DoneCommand : ITaskStakCommand
    {
        public const string Name = TaskStakCommands.Done;
        public const string Description = Descriptions.TaskStakCommands.Done;

        public static Command Create()
        {
            var titleArg = new Argument<string[]>(Arguments.Title, Descriptions.Arguments.Title)
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
                Console.WriteLine(Messages.TaskNotFound);
                return;
            }

            if (count > 1)
            {
                Console.WriteLine(Messages.MultipleTasksFound);
                return;
            }

            var task = tasks.First(t => t.Id == filtered.First().Id);

            task.Complete();
            JsonHelper.SaveTasks(tasks);

            Console.WriteLine(Messages.TaskCompleted, task.Title);
            return;

            bool FilterPredicate(TaskEntry tsk) => tsk.IsActive && 
                                                    titleArgs.All(arg => tsk.Title.Contains(arg, StringComparison.OrdinalIgnoreCase));
        }
    }
}
