namespace TaskStak.CLI.Utils
{
    public static class Arguments
    {
        public const string Title = "title";
        public const string View = "view";
    }

    public static class Descriptions
    {
        public static class TaskStakCommands
        {
            public const string Add = "Add a task to your stak.";
            public const string Done = "Mark a task complete.";
            public const string List = "List current tasks in stak.";
        }

        public static class Arguments
        {
            public const string Title = "The title of the task.";
            public const string View = "The specific view to use for listing tasks. Defaults to 'Day'.";
        }

        public static class Options
        {
            public const string Status = "The status of the task to add. Defaults to 'Active'.";
        }
    }

    public static class Messages
    {
        public const string TaskAdded = "✅ Task added successfully.";
        public const string TaskCompleted = "✅ {0} completed successfully.";
        public const string TaskNotFound = "❌ Task not found. Please check the title and try again.";
        public const string MultipleTasksFound = "❌ Multiple tasks found. Please be more specific.";
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
        public const string Done = "done";
        public const string List = "list";
        public const string ListAlias = "ls";
    }
}
