using System.CommandLine;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class TitleCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Title;
        public static string Description => Constants.Commands.Descriptions.TitleDesc;

        public static Command Create()
        {
            var queryArg = new Argument<string>(Constants.Arguments.Query, Constants.Arguments.Descriptions.QueryDesc);
            var titleArg = new Argument<string>(Constants.Arguments.Title, Constants.Arguments.Descriptions.TitleDesc);

            var command = new Command(Name, Description)
            {
                queryArg,
                titleArg,
            };

            command.SetHandler(Execute, queryArg, titleArg);
            return command;
        }

        public static void Execute(string queryArg, string titleArg)
        {
            var command = new TaskSearchCommand();
            var criteria = new TaskSearchCriteria
            {
                Query = queryArg,
                StatusFlags = TaskEntryStatus.All,
            };

            command
                .WithCriteria(criteria)
                .OnTaskFound((tasks, task) =>
                {
                    var original = task.Title;

                    task.Title = titleArg;
                    JsonHelper.SaveTasks(tasks);

                    Console.WriteLine(Constants.Messages.TaskUpdated, nameof(task.Title).ToLowerInvariant(), original, titleArg);
                });

            command.Execute();
        }
    }
}
