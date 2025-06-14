using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;

namespace TaskStak.CLI.Presentation.Sections
{
    public class BlockedSection(IEnumerable<TaskEntry> tasks, ITaskStakFormatter<TaskEntry> formatter) : ISectionView
    {
        public string Title => "Blocked Tasks";

        public void Render()
        {
            if (!tasks.Any())
            {
                this.NoContent();
                return;
            }

            this.RenderHeader();
            
            foreach (var task in tasks)
            {
                Console.WriteLine(formatter.Format(task));
            }

            Console.WriteLine();
        }

        public void RenderHeader()
        {
            Console.WriteLine($"{this.Title} ({tasks.Count()})");
        }

        public void NoContent()
        { }
    }
}
