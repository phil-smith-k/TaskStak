using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Views
{
    public class CandidatesView : ITaskView
    {
        public string Title => Constants.Messages.CandidatesFound;

        public void NoContent()
        { }

        public void RenderTasks(IEnumerable<TaskEntry> tasks)
        {
            Console.Out.WriteLineColor($"{this.Title}", ConsoleColor.DarkYellow);

            var formatter = new VerboseTaskFormatter();
            foreach (var task in tasks) 
            { 
                Console.Out.WriteLineColor(formatter.Format(task), ConsoleColor.DarkYellow);
            }
        }
    }
}
