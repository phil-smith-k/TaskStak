using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Formatters;
using TaskStak.CLI.Presentation.Sections;

namespace TaskStak.CLI.Presentation.Views
{

    public class UnstagedView(ListOptions options) : ITaskView
    {
        private readonly ITaskStakFormatter<string> _headerFormatter = new HeaderFormatter();

        public string Title => "Unstaged tasks";

        public void NoContent()
        {
            Console.WriteLine(@"Nothing to see here. ¯\_(ツ)_/¯");
        }

        public void RenderTasks(IEnumerable<TaskEntry> tasks)
        {
            var unstagedTasks = tasks.Where(tsk => !tsk.IsStaged && tsk.IsActive).ToList();

            if (!unstagedTasks.Any()) 
            {
                this.NoContent();
                return;
            }

            var verbose = options.Verbose;
            ITaskStakFormatter<TaskEntry> formatter = verbose 
                ? new VerboseTaskFormatter() 
                : new ActiveTaskFormatter();

            ISectionView section = new ActiveTasksSection(unstagedTasks, formatter);

            Console.WriteLine(_headerFormatter.Format(this.Title));
            section.Render();
        }
    }
}
