using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;

namespace TaskStak.CLI.Presentation.Sections
{
    public class ActiveTasksSection(IEnumerable<TaskEntry> tasks, ITaskStakFormatter<TaskEntry> formatter) : ISectionView
    {
        public string Title => "Active Tasks";

        public void Render()
        {
            if (!tasks.Any())
            {
                this.NoContent();
                return;
            }

            Console.WriteLine(this.GetHeader());
            foreach (var task in tasks.OrderByDescending(tsk => tsk.Timeline.CreatedOn))
            {
                Console.WriteLine(formatter.Format(task));
            }

            Console.WriteLine();
        }

        public string GetHeader()
            => $"{this.Title} ({tasks.Count()})";

        public void NoContent()
        {
            Console.WriteLine($"{this.GetHeader()} - All caught up! Nice work!");
            Console.WriteLine();
        }
    }
}
