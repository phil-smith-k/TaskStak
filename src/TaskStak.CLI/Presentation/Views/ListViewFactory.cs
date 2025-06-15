using TaskStak.CLI.Models;

namespace TaskStak.CLI.Presentation.Views
{
    public class ListViewFactory
    {
        public static IListView GetViewFor(ListOptions? options = null)
        {
            options ??= new ListOptions(); 

            return options.ViewOption switch
            {
                ViewOption.Day => new DayView(options),
                ViewOption.Verbose => new VerboseView(options),

                _ => new DayView(options),
            };
        }

        public static IListView GetViewFor(ViewOption arg)
        {
            var options = new ListOptions { ViewOption = arg };

            return GetViewFor(options);
        }
    }
}
