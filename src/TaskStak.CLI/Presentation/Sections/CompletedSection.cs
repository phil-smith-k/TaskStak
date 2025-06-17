using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Sections
{
    public class CompletedSection(IEnumerable<TaskEntry> tasks, ITaskStakFormatter<TaskEntry> formatter) : ISectionView
    {
        public string Title => "Completed Tasks";

        private bool AnyTasks => tasks.Any();

        public void Render()
        {
            if (!this.AnyTasks)
            {
                this.NoContent();
                return;
            }

            Console.WriteLine(this.GetHeader());
            foreach (var task in tasks.OrderByDescending(tsk => tsk.Timeline.CompletedOn))
            {
                Console.WriteLine(formatter.Format(task));
            }

            Console.WriteLine();
        }

        public string GetHeader()
            => $"{this.Title} ({tasks.Count()})";

        public void NoContent()
        {
            Console.WriteLine(this.GetHeader());
            Console.WriteLine();
        }
    }
}
