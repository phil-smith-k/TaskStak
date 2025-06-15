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
            var (active, blocked, complete) = GroupByStatus(tasks);

            ISectionView[] sections =
            [
                new ActiveTasksSection(active, new ActiveTaskFormatter()),
                new BlockedSection(blocked, new BlockedTaskFormatter()),
                new CompletedSection(complete, new CompletedTaskFormatter())
            ];

            Array.ForEach(sections, section => section.Render());
        }

        private static (List<TaskEntry> Active, List<TaskEntry> Blocked, List<TaskEntry> Completed) GroupByStatus(IEnumerable<TaskEntry> tasks)
        {
            var active = new List<TaskEntry>();
            var blocked = new List<TaskEntry>();
            var completed = new List<TaskEntry>();

            foreach (var task in tasks)
            {
                if (task.IsActive)
                {
                    active.Add(task);
                }
                else if (task.IsBlocked)
                {
                    blocked.Add(task);
                }
                else if (task.IsCompleted)
                {
                    completed.Add(task);
                }
            }

            return (active, blocked, completed);
        }
    }
}
