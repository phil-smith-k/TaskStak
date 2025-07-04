﻿using TaskStak.CLI.Persistence.Models;

namespace TaskStak.CLI.Utils
{
    public static class InboundMapper
    {
        public static TaskEntry Map(Models.TaskEntry source)
        {
            return new TaskEntry
            {
                Id = source.Id,
                Title = source.Title,
                Status = (int)source.Status,
                Timeline = Map(source.Timeline),
            };
        }

        public static Timeline Map(Models.Timeline source)
        {
            return new Timeline
            {
                CreatedOn = source.CreatedOn,
                CompletedOn = source.CompletedOn,
                LastModifiedOn = source.LastModifiedOn,
                StagedFor = source.StagedFor,
            };
        }
    }
}
