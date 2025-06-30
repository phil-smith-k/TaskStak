using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class RemoveCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Remove;
        public static string Description => Constants.Commands.Descriptions.RemoveDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string>(Constants.Arguments.Query, Constants.Arguments.Descriptions.QueryDesc);

            var command = new Command(Name, Description)
            {
                queryArg,
            };

            command.SetHandler(Execute, queryArg);
            return command;
        }

        public static void Execute(string queryArg)
        {
            var command = new TaskSearchCommand();
            var criteria = new TaskSearchCriteria()
            {
                Query = queryArg,
                StatusFlags = TaskEntryStatus.All,
            };

            command
                .WithCriteria(criteria)
                .OnTaskFound((tasks, task) =>
                {
                    tasks.Remove(task);

                    JsonHelper.SaveTasks(tasks);

                    Console.WriteLine(Constants.Messages.TaskRemoved, task.Id, task.Title);
                });

            command.Execute();
        }
    }
}
