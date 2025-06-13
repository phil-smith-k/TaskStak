using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Views
{
    public class ListViewFactory
    {
        public static IListView GetViewFor(ListOptions? options = null)
        {
            options ??= new ListOptions(); 

            return options.ViewArgument switch
            {
                _ => new DefaultListView(options)
            };
        }
    }
}
