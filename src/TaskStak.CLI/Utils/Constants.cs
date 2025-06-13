namespace TaskStak.CLI.Utils
{
    public static class Arguments
    {
        public const string Title = "title";
    }

    public static class Descriptions
    {
        public static class TaskStakCommands
        {
            public const string Add = "Add a task to your stak.";
            public const string List = "List current tasks in stak.";
        }

        public static class Arguments
        {
            public const string Title = "The title of the task to add.";
        }

        public static class Options
        {
            public const string Status = "The status of the task to add. Defaults to 'Active'.";
        }
    }

    public static class Messages
    {
        public const string TaskAdded = "✅ Task added successfully.";
        public const string NoTasksFound = "❌ No tasks found.";
    }

    public static class Options
    {
        public const string Status = "--status";
        public const string StatusAlias = "-s";
    }

    public static class TaskStakCommands
    {
        public const string Add = "add";
        public const string List = "ls";
    }
}
