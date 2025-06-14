using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Sections
{
    public class ActiveTasksSection(IEnumerable<TaskEntry> tasks, ITaskStakFormatter<TaskEntry> formatter) : ISectionView
    {
        public string Title => "Active Tasks";

        public void Render()
        {
            this.RenderHeader();

            if (!tasks.Any())
            {
                this.NoContent();
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine(formatter.Format(task));
            }
        }

        public void RenderHeader()
        {
            Console.WriteLine($"{this.Title}: ({tasks.Count()})");
        }

        public void NoContent()
        {
            Console.WriteLine($"{Constants.Emojis.Star} Nice work! {Constants.Emojis.Star}");
        }
    }
}
