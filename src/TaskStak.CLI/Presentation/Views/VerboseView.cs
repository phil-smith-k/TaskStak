using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;

namespace TaskStak.CLI.Presentation.Views
{
    public class VerboseView(ListOptions options) : IListView
    {
        private readonly ListOptions _options = options;

        public void Render(IEnumerable<TaskEntry> tasks)
        {
            var formatter = new VerboseTaskFormatter();

            foreach (var task in tasks.OrderByDescending(t => t.Timeline.CreatedOn))
            {
                Console.WriteLine(formatter.Format(task));
            }
        }
    }
}
