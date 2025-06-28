using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Sections
{
    public class OverdueSection(IEnumerable<TaskEntry> tasks, ITaskStakFormatter<TaskEntry> formatter) : ISectionView
    {
        private readonly ConsoleColor _overdueColor = ConsoleColor.DarkYellow;

        public string Title => Constants.DisplaySymbol.Warning;

        public void Render()
        {
            if (!tasks.Any())
            {
                this.NoContent();
                return;
            }

            Console.Out.WriteLineColor(this.GetHeader(), _overdueColor);
            foreach (var task in tasks.OrderByDescending(tsk => tsk.Timeline.StagedFor))
            {
                Console.Out.WriteLineColor(formatter.Format(task), _overdueColor);
            }

            Console.WriteLine();
        }

        public void NoContent()
        { }

        public string GetHeader()
        {
            var count = tasks.Count();
            return $"{this.Title} {count} {(count == 1 ? "task needs" : "tasks need")} your attention...";
        } 
    }
}
