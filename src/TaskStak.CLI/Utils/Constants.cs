namespace TaskStak.CLI.Utils
{
    public class Constants
    {
        public static class Arguments
        {
            public const string Title = "title";
            public const string View = "view";

            public static class Descriptions
            {
                public const string TitleDesc = "The title of the task.";
                public const string ViewDesc = "The specific view to use for listing tasks. Defaults to 'Day'.";
            }
        }

        public static class Commands
        {
            public const string Add = "add";
            public const string Done = "done";
            public const string View = "view";
            public const string ViewAlias = "v";

            public static class Descriptions
            {
                public const string AddDesc = "Add a task to your stak.";
                public const string DoneDesc = "Mark a task complete.";
                public const string ViewDesc = "View current tasks in stak.";
            }
        }

        public static class Emojis
        {
            public const string Success = "\U00002705";     // ✅
            public const string Error = "\U0000274C";       // ❌
            public const string Warning = "\U000026A0";     // ⚠️
            public const string Info = "\U00002139";        // ℹ️
            public const string Star = "\U00002B50";        // ⭐
        }

        public static class Messages
        {
            public const string TaskAdded = $"{Emojis.Success} Task added successfully {Emojis.Success} ";
            public const string TaskCompleted = $"{Emojis.Success} {{0}} completed successfully {Emojis.Success} ";
            public const string TaskNotFound = $"{Emojis.Error} Task not found. Please check the title and try again {Emojis.Error}";
            public const string MultipleTasksFound = $"{Emojis.Warning} Multiple tasks found. Please be more specific {Emojis.Warning}";
            public const string NoTasksFound = $"{Emojis.Error} No tasks found {Emojis.Error} ";
        }

        public static class Options
        {
            public const string Status = "--status";
            public const string StatusAlias = "-s";

            public static class Descriptions
            {
                public const string StatusDesc = "The status of the task to add. Defaults to 'Active'.";
            }
        }
    }
}
