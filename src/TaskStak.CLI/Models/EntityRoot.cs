namespace TaskStak.CLI.Models
{
    public abstract class EntityRoot
    {
        public EntityId Id { get; init; } = EntityId.New();
    }
}
