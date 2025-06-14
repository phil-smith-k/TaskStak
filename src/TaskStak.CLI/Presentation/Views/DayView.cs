using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Presentation.Sections;

namespace TaskStak.CLI.Presentation.Views
{
    public class DayView(ListOptions options) : IListView
    {
        private readonly ListOptions _options = options;

        public void Render(IEnumerable<TaskEntry> tasks)
        {
            var (active, complete) = FilterTasks(tasks);

            ISectionView[] sections =
            [
                new ActiveTasksSection(active, new ActiveTaskFormatter()),
                new CompletedSection(complete, new CompletedTaskFormatter())
            ];

            Array.ForEach(sections, section =>
            {
                section.Render();
                Console.WriteLine();
            });
        }

        private static (List<TaskEntry> Active, List<TaskEntry> Complete) FilterTasks(IEnumerable<TaskEntry> tasks)
        {
            var active = new List<TaskEntry>();
            var complete = new List<TaskEntry>();

            foreach (var task in tasks)
            {
                if (task.IsActive)
                    active.Add(task);
                else if (task.IsComplete)
                    complete.Add(task);
            }

            return (active, complete);
        }
    }
}
