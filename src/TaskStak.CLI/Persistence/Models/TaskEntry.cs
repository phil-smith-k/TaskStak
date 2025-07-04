﻿namespace TaskStak.CLI.Persistence.Models
{
    public class TaskEntry
    {
        public required string Id { get; set; }

        public int Status { get; set; }

        public required string Title { get; set; }

        public required Timeline Timeline { get; set; }
    }
}
