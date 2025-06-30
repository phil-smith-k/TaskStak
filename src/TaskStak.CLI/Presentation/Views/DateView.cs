using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Presentation.Sections;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Presentation.Views
{
    public class DateView : ITaskView
    {
        private readonly ListOptions _options;
        private readonly ITaskStakFormatter<string> _headerFormatter = new HeaderFormatter();

        public string Title => $"{(_options.Date!.Value.AddDays(-1).IsToday() ? "Tomorrow" : _options.Date!.Value.ToString("ddd MMMM dd, yyyy"))}";

        public DateView(ListOptions options)
        {
            if (!options.Date.HasValue)
                throw new ArgumentException($"Date option must be provided for {nameof(DateView)}.", nameof(options.Date));

            _options = options;
        }

        public void RenderTasks(IEnumerable<TaskEntry> tasks)
        {
            var (active, completed) = GroupByStatus(tasks);

            List<ISectionView> sections = [];

            var hasActive = active.Any();
            var hasCompleted = completed.Any();

            var verbose = _options.Verbose;

            if (!hasActive && !hasCompleted)
            {
                NoContent();
                return;
            }

            if (hasActive)
            {
                ITaskStakFormatter<TaskEntry> formatter = verbose ? new VerboseTaskFormatter() : new ActiveTaskFormatter();
                sections.Add(new ActiveTasksSection(active, formatter));
            }

            if (hasCompleted)
            {
                ITaskStakFormatter<TaskEntry> formatter = verbose ? new VerboseTaskFormatter() : new CompletedTaskFormatter();
                sections.Add(new CompletedSection(completed, formatter));
            }

            Console.WriteLine(_headerFormatter.Format(this.Title));
            sections.ForEach(section => section.Render());
        }

        public void NoContent()
        {
            Console.WriteLine(@"Nothing to see here. ¯\_(ツ)_/¯");
        }

        private (List<TaskEntry> Active, List<TaskEntry> Completed) GroupByStatus(IEnumerable<TaskEntry> tasks)
        {
            var active = new List<TaskEntry>();
            var completed = new List<TaskEntry>();

            foreach (var task in tasks)
            {
                if (task.IsActive && task.IsStagedFor(_options.Date!.Value))
                {
                    active.Add(task);
                    continue;
                }

                var completedDate = DateOnly.FromDateTime(task.Timeline.CompletedOn?.Date ?? DateTime.MinValue);
                if (task.IsCompleted && completedDate == _options.Date)
                {
                    completed.Add(task);
                    continue;
                }
            }
            return (active, completed);
        }
    }
}
