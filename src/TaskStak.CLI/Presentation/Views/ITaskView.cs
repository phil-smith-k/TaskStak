using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Views
{
    public interface ITaskView
    {
        string Title { get; }

        void RenderTasks(IEnumerable<TaskEntry> tasks);

        void NoContent();
    }
}
