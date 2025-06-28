namespace TaskStak.CLI.Utils
{
    public class Constants
    {
        public static class Arguments
        {
            public const string Date = "date";
            public const string Query = "query";
            public const string Title = "title";

            public static class Descriptions
            {
                public const string DateDesc = "Represents a date. Use a date format or use --today, --tomorrow, --monday, --tuesday etc. Default is today's date.";
                public const string QueryDesc = "Search query argument. Use id or title of task.";
                public const string TitleDesc = "The title of the new or updated task.";
            }
        }

        public static class Commands
        {
            public const string Add = "add";
            public const string Done = "done";
            public const string Move = "move";
            public const string Remove = "rm";
            public const string Stak = "stak";
            public const string Title = "title";
            public const string View = "view";

            public static class Descriptions
            {
                public const string AddDesc = "Add a task to your stak.";
                public const string DoneDesc = "Mark a task complete.";
                public const string MoveDesc = "Move a task to a different status.";
                public const string RemoveDesc = "Removes a task completely. Cannot be undone.";
                public const string StakDesc = "Stage a task to a task stak for a particular date.";
                public const string TitleDesc = "Update the title of an existing task.";
                public const string ViewDesc = "View current tasks in stak.";
            }
        }

        public static class DisplaySymbol
        {
            public const string Success = "\U00002705";     // ✅
            public const string Error = "\U0000274C";       // ❌
            public const string Warning = "\U000026A0";     // ⚠️
            public const string Info = "\U00002139";        // ℹ️
            public const string Active = "[ ]";       
            public const string Blocked = "[!]";
            public const string Complete = "[x]";
        }

        public static class FilePaths
        {
            public const string Directory = ".taskStak";
            public const string FileName = "tasks.json";
        }

        public static class Messages
        {
            public const string TaskAdded = $"{DisplaySymbol.Success} '{{0}}' added successfully.";
            public const string TaskCompleted = $"{DisplaySymbol.Success} '{{0}}' completed successfully.";
            public const string TaskUpdated = $"{DisplaySymbol.Success} Task '{{0}}' updated from '{{1}}' to '{{2}}'";
            public const string TaskAddedToStak = $"{DisplaySymbol.Success} Task '{{0}}' staged for {{1}} {{2}}";
            public const string TaskRemoved = $"{DisplaySymbol.Success} Task '{{0}}' removed.";
            public const string CandidatesFound = $"{DisplaySymbol.Warning} Multiple tasks found. Refine your query or use 8-digit task identifier.";
            public const string NoTasksFound = $"{DisplaySymbol.Error} No tasks found.";
            public const string QueryNotFoundInStatus = $"{DisplaySymbol.Error} Query '{{0}}' found no results in {{1}} status.";
        }

        public static class Options
        {
            public const string InDay = "--in";
            public const string Status = "--status";
            public const string StatusAlias = "-s";

            public const string Verbose = "--verbose";
            public const string VerboseAlias = "-v";

            // Day flags - used for date argument
            public const string Today = "--today";
            public const string TodayAlias = "-t";

            public const string Tomorrow = "--tomorrow";
            public const string TomorrowAlias = "-tm";

            public const string Monday = "--monday";
            public const string MondayAlias = "-mon";

            public const string Tuesday = "--tuesday";
            public const string TuesdayAlias = "-tue";

            public const string Wednesday = "--wednesday";
            public const string WednesdayAlias = "-wed";

            public const string Thursday = "--thursday";
            public const string ThursdayAlias = "-thu";

            public const string Friday = "--friday";
            public const string FridayAlias = "-fri";

            public const string Saturday = "--saturday";
            public const string SaturdayAlias = "-sat";

            public const string Sunday = "--sunday";
            public const string SundayAlias = "-sun";

            public static class Descriptions
            {
                public const string InDayDesc = "Add to stak N days from the current date.";
                public const string StatusDesc = "The status of the task to add. Defaults to 'Active'.";
                public const string VerboseDesc = "When option is present, displays more task details.";
            }
        }
    }
}
