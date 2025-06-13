using TaskStak.CLI.Models;

namespace TaskStak.CLI.Utils
{
    public static class OutboundMapper
    {
        public static TaskEntry Map(Persistence.Models.TaskEntry source)
        {
            // TECH DEBT: null-forgiving is used here, but not null should be guaranteed by the persistence model.
            return new TaskEntry(source.Title!, (TaskEntryStatus)source.Status)
            {
                Id = source.Id,
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
