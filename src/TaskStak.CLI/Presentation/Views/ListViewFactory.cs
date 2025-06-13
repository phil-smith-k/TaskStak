using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Views
{
    public class ListViewFactory
    {
        public static IListView GetViewFor(ListViewType type, ListOptions? options = null)
        {
            return type switch
            {
                _ => new DefaultListView(options ?? new())
            };
        }
    }
}
