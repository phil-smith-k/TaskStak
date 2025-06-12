namespace TaskStak.CLI.Models
{
    public class TaskEntry
    {

#region Constructors
        public TaskEntry(string title)
        {
            this.Title = title;
            this.Timeline = Timeline.Begin();

            this.Status = Flags<TaskEntryStatus>.From(TaskEntryStatus.Active);
        }
#endregion

#region Properties
        public Flags<TaskEntryStatus> Status { get; set; }

        public Timeline Timeline { get; set; }

        public string? Title { get; set; }

        public bool IsActive
            => this.Status.IsOn(TaskEntryStatus.Active);

        public bool IsImpeded
            => this.Status.IsOn(TaskEntryStatus.Impeded);

        public bool IsComplete
            => this.Status.IsOn(TaskEntryStatus.Completed);
#endregion

#region Public Methods
        public void Complete()
        {
            this.Timeline.End();
            this.Status.SetTo(TaskEntryStatus.Completed);
        }
#endregion  

    }
}
