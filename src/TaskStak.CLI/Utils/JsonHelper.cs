using System.Text.Json;
using TaskStak.CLI.Models;

namespace TaskStak.CLI.Utils
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            MaxDepth = 64,
        };

        private static readonly string TasksFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".task",
            "tasks.json"
        );

        public static List<TaskEntry> LoadTasks()
        {
            return LoadTasks(t => true);
        }

        public static List<TaskEntry> LoadTasks(Func<TaskEntry, bool> predicate)
        {
            if (!File.Exists(TasksFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TasksFilePath)!);
                return [];
            }

            var json = File.ReadAllText(TasksFilePath);
            var tasks = JsonSerializer.Deserialize<Persistence.Models.TaskEntry[]>(json, JsonOptions) ?? [];

            return tasks
                .Select(OutboundMapper.Map)
                .Where(predicate)
                .ToList();
        }

        public static void SaveTasks(List<TaskEntry> tasks)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(TasksFilePath)!);

            var models = tasks.Select(InboundMapper.Map).ToArray();
            var json = JsonSerializer.Serialize(models, JsonOptions);

            File.WriteAllText(TasksFilePath, json);
        }
    }
}
