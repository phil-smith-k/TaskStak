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
            this.RenderHeader();

            if (!this.AnyTasks)
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
            if (!this.AnyTasks)
            {
                Console.WriteLine($"{this.Title}");
                return;
            }

            Console.WriteLine($"{this.Title} ({tasks.Count()})");
        }

        public void NoContent()
        {
            Console.WriteLine($"    {Constants.DisplaySymbol.Warning} Nothing completed yet - time to get started!");
        }
    }
}
