using System.CommandLine;
using System.Globalization;
using TaskStak.CLI.Models;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class PopCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.Pop;
        public static string Description => Constants.Commands.Descriptions.PopDesc;

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

        private static void Execute(string queryArg) 
        { 
            var searchCommand = new TaskSearchCommand();
            var criteria = new TaskSearchCriteria
            {
                Query = queryArg, StatusFlags = TaskEntryStatus.Active | TaskEntryStatus.Blocked 
            };

            searchCommand
                .WithCriteria(criteria)
                .OnTaskFound((tasks, task) =>
                {
                    if (!task.Timeline.StagedFor.HasValue)
                    {
                        return;
                    }

                    var dateStagedFor = task.Timeline.StagedFor.Value;

                    task.Unstage();
                    JsonHelper.SaveTasks(tasks);

                    Console.WriteLine(Constants.Messages.TaskPopped, task.Title, dateStagedFor.ToString("ddd", CultureInfo.CurrentCulture), dateStagedFor.ToString("d", CultureInfo.CurrentCulture));
                });

            searchCommand.Execute();
        }
    }
}
