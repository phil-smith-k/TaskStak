namespace TaskStak.CLI.Models
{
    public class TaskEntry : EntityRoot
    {
        public TaskEntry(string title, TaskEntryStatus status = TaskEntryStatus.Active)
        {
            this.Title = title;
            this.Timeline = Timeline.Begin();
            this.Status = Flags<TaskEntryStatus>.From(status);

            if (this.IsBlocked)
            {
                this.Block();
            }
            else if (this.IsComplete)
            {
                this.Complete();
            }
        }

#region Properties
        public Flags<TaskEntryStatus> Status { get; set; } 

        public Timeline Timeline { get; set; }

        public string Title { get; set; }

        public bool IsActive
            => this.Status.IsOn(TaskEntryStatus.Active);

        public bool IsBlocked
            => this.Status.IsOn(TaskEntryStatus.Blocked);

        public bool IsComplete
            => this.Status.IsOn(TaskEntryStatus.Completed);
#endregion

#region Public Methods
        public void Complete()
        {
            this.Timeline.End();
            this.Timeline.StatusChangedOn = DateTime.UtcNow;

            this.Status.SetTo(TaskEntryStatus.Completed);
        }

        public void Block()
        {
            var now = DateTime.UtcNow;
            this.Timeline.StatusChangedOn = now;
            this.Timeline.LastModifiedOn = now;

            this.Status.SetOn(TaskEntryStatus.Blocked);
        }

        public void Unblock()
        {
            var now = DateTime.UtcNow;
            this.Timeline.StatusChangedOn = now;
            this.Timeline.LastModifiedOn = now;

            this.Status.SetOff(TaskEntryStatus.Blocked);
        }
        #endregion
    }
}
