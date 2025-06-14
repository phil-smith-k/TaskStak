using System.Text;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;

namespace TaskStak.CLI.Presentation.Sections
{
    public class ActiveTasksSection(IEnumerable<TaskEntry> tasks) : ISectionView
    {
        public string Title => "Active Tasks";

        public void Render()
        {
            this.RenderHeader();

            foreach (var taskEntry in tasks)
            {
                Console.WriteLine($"{taskEntry.Id}   {taskEntry.Title} [{taskEntry.Timeline.CreatedOn}]");
            }
        }

        public void RenderHeader()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("------");
            builder.AppendLine(this.Title);
            builder.AppendLine("------");

            Console.WriteLine(builder.ToString());
        }
    }
}
