using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Views
{
    public class DefaultListView(ListOptions options) : IListView
    {
        private readonly ListOptions _options = options;

        public void Render(IEnumerable<TaskEntry> tasks)
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Status.Value}: {task.Title} - (Created: {task.Timeline.CreatedOn})");
            }
        }
    }
}
