namespace TaskStak.CLI.Models
{
    public abstract class EntityRoot
    {
        public string Id { get; init; } = Utils.LookupKey.New();
    }
}
