using TaskStak.CLI.Models;

namespace TaskStak.CLI.Utils
{
    public static class OutboundMapper
    {
        public static TaskEntry Map(Persistence.Models.TaskEntry source)
        {
            var task = new TaskEntry
            {
                Id = EntityId.Parse(source.Id),
                Timeline = Map(source.Timeline),
                Title = source.Title,
            };

            task.SetStatusTo((TaskEntryStatus)source.Status);
            return task;
        }

        public static Timeline Map(Persistence.Models.Timeline source)
        {
            return new Timeline
            {
                CreatedOn = source.CreatedOn.ToLocalTime(),
                CompletedOn = source.CompletedOn?.ToLocalTime(),
                LastModifiedOn = source.LastModifiedOn?.ToLocalTime(),
                StagedFor = source.StagedFor,
            };
        }
    }
}
