namespace TaskStak.CLI.Utils
{
    public class Constants
    {
        public static class Arguments
        {
            public const string Query = "query";
            public const string Title = "title";

            public static class Descriptions
            {
                public const string QueryDesc = "Search query argument. Use id or title of task.";
                public const string TitleDesc = "The title of the new or updated task.";
            }
        }

        public static class Commands
        {
            public const string Add = "add";
            public const string Done = "done";
            public const string Move = "move";
            public const string Title = "title";
            public const string View = "view";

            public static class Descriptions
            {
                public const string AddDesc = "Add a task to your stak.";
                public const string DoneDesc = "Mark a task complete.";
                public const string MoveDesc = "Move a task to a different status.";
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
            public const string Active = "*";       
            public const string Blocked = "!";
            public const string Complete = "x";
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
            public const string TaskUpdated = $"{DisplaySymbol.Success} Task {{0}} updated from '{{1}}' to '{{2}}'";
            public const string MultipleTasksFound = $"{DisplaySymbol.Warning} Multiple tasks found. Please be more specific.";
            public const string NoTasksFound = $"{DisplaySymbol.Error} No tasks found.";
        }

        public static class Options
        {
            public const string Status = "--status";
            public const string StatusAlias = "-s";
            public const string View = "--view";
            public const string ViewAlias = "-v";

            public static class Descriptions
            {
                public const string StatusDesc = "The status of the task to add. Defaults to 'Active'.";
                public const string ViewDesc = "The view to use when listing tasks. Defaults to 'Day'. Case insensitive. First letter abberviation accepted (ex. 'task view -v w' for 'Week')";
            }
        }
    }
}
