using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Sections
{
    public class NeedsAttentionSection(IEnumerable<TaskEntry> tasks, ITaskStakFormatter<TaskEntry> formatter) : ISectionView
    {
        private readonly ConsoleColor _attentionColor = ConsoleColor.DarkYellow;

        public string Title => Constants.DisplaySymbol.Warning;

        public void Render()
        {
            if (!tasks.Any())
            {
                this.NoContent();
                return;
            }

            Console.Out.WriteLineColor(this.GetHeader(), _attentionColor);
            foreach (var task in tasks.OrderByDescending(tsk => tsk.Timeline.StakDate ?? DateOnly.FromDateTime(tsk.Timeline.CreatedOn.Date)))
            {
                Console.Out.WriteLineColor(formatter.Format(task), _attentionColor);
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
