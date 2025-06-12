namespace TaskStak.CLI.Models
{
    public class TaskEntry(string title, TaskEntryStatus status = TaskEntryStatus.Active)
    {
#region Properties
        public Flags<TaskEntryStatus> Status { get; set; } = Flags<TaskEntryStatus>.From(status);

        public Timeline Timeline { get; set; } = Timeline.Begin();

        public string Title { get; set; } = title;

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
