using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Views
{
    public interface IListView
    {
        void Render(IEnumerable<TaskEntry> tasks);
    }
}
