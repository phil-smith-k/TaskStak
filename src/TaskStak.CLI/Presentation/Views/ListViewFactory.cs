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
                ViewArgument.Day => new DayView(options),
                ViewArgument.Verbose => new VerboseView(options),

                _ => new DayView(options),
            };
        }

        public static IListView GetViewFor(ViewArgument arg)
        {
            var options = new ListOptions { ViewArgument = arg };

            return GetViewFor(options);
        }
    }
}
