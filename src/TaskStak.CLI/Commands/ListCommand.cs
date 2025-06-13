using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
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
            var tasks = JsonHelper.LoadTasks();

            if (tasks.Count == 0)
            {
                Console.WriteLine(Messages.NoTasksFound);
                return;
            }

            var view = ListViewFactory.GetViewFor(ListViewType.Default);
            view.Render(tasks);
        }
    }
}
