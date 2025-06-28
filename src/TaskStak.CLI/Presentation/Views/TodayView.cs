using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Presentation.Sections;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Views
{
    public class TodayView : ITaskView
    {
        private readonly ListOptions _options;
        private readonly ITaskStakFormatter<string> _headerFormatter = new HeaderFormatter();

        public string Title => $"Today";

        public TodayView(ListOptions options)
        {
            if (!options.Date.IsToday())
                throw new ArgumentException($"Date option must be set to today for {nameof(TodayView)}.", nameof(options.Date));

            _options = options;
        }

        public void RenderTasks(IEnumerable<TaskEntry> tasks)
        {
            tasks = tasks.ToList();
            if (!tasks.Any())
            {
                NoContent();
                return;
            }

            var verbose = _options.Verbose;
            var (active, blocked, completed) = GroupByStatus(tasks);

            List<ISectionView> sections =
            [
                new ActiveTasksSection(active, verbose ? new VerboseTaskFormatter() : new ActiveTaskFormatter()),
                new BlockedSection(blocked, verbose ? new VerboseTaskFormatter() : new BlockedTaskFormatter()),
                new CompletedSection(completed, verbose ? new VerboseTaskFormatter() : new CompletedTaskFormatter())
            ];

            var overdue = tasks.Where(task => task.IsOverdue && !task.IsBlocked).ToList();
            if (overdue.Any())
            {
                sections.Insert(0, new OverdueSection(overdue, verbose ? new VerboseTaskFormatter() : new OverdueTaskFormatter()));
            }

            Console.WriteLine(_headerFormatter.Format(this.Title));
            sections.ForEach(section => section.Render());
        }

        public void NoContent()
        {
            Console.WriteLine(@"Nothing to see here. ¯\_(ツ)_/¯");
        }

        private static (List<TaskEntry> Active, List<TaskEntry> Blocked, List<TaskEntry> Completed) GroupByStatus(IEnumerable<TaskEntry> tasks)
        {
            var active = new List<TaskEntry>();
            var blocked = new List<TaskEntry>();
            var completed = new List<TaskEntry>();

            foreach (var task in tasks)
            {
                if (task.IsActive && task.IsStagedFor(DateTime.Today))
                {
                    active.Add(task);
                }
                else if (task.IsBlocked)
                {
                    blocked.Add(task);
                }
                else if (task.IsCompleted && task.Timeline.CompletedOn.IsToday())
                {
                    completed.Add(task);
                }
            }

            return (active, blocked, completed);
        }
    }
}
