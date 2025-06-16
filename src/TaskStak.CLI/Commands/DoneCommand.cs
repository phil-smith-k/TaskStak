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
            var criteria = new TaskSearchCriteria { Query = queryArg };
            var searchCommand = new TaskSearchCommand();

            searchCommand
                .WithCriteria(criteria)
                .OnTaskFound((tasks, task) =>
                {
                    task.Complete();
                    JsonHelper.SaveTasks(tasks);

                    Console.WriteLine(Constants.Messages.TaskCompleted, task.Title);
                });

            searchCommand.Execute();
        }
    }
}
