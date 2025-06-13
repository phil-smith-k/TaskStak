using System.CommandLine;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class ListCommand : ITaskStakCommand
    {
        public const string Name = TaskStakCommands.List;
        public const string Description = Descriptions.TaskStakCommands.List;

        public static Command Create()
        {
            var command = new Command(Name, Description);
            command.SetHandler(Execute);

            return command;
        }

        public static void Execute()
        {
            var tasks = JsonHelper.LoadTasks(t => t.IsActive);

            if (tasks.Count == 0)
            {
                Console.WriteLine(Messages.NoTasksFound);
                return;
            }

            tasks.ForEach(task => Console.WriteLine($"{task.Status.Value}: {task.Title} - (Created: {task.Timeline.CreatedOn})"));
        }
    }
}
