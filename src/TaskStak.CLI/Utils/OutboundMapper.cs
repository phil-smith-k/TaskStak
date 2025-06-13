using TaskStak.CLI.Models;

namespace TaskStak.CLI.Utils
{
    public static class OutboundMapper
    {
        public static TaskEntry Map(Persistence.Models.TaskEntry source)
        {
            return new TaskEntry(source.Title, (TaskEntryStatus)source.Status)
            {
                Id = EntityId.Parse(source.Id),
                Timeline = Map(source.Timeline!),
            };
        }

        public static Timeline Map(Persistence.Models.Timeline source)
        {
            return new Timeline
            {
                CreatedOn = source.CreatedOn,
                CompletedOn = source.CompletedOn,
                LastModifiedOn = source.LastModifiedOn,
            };
        }
    }
}
