namespace TaskStak.CLI.Models
{
    public class TaskEntry : EntityRoot
    {
        #region Properties
        public Flags<TaskEntryStatus> Status { get; set; } = Flags<TaskEntryStatus>.From(TaskEntryStatus.Active);

        public Timeline Timeline { get; set; } = Timeline.Begin();

        public required string Title { get; set; }

        public bool IsActive
            => this.Status.IsOn(TaskEntryStatus.Active);

        public bool IsBlocked
            => this.Status.IsOn(TaskEntryStatus.Blocked);

        public bool IsComplete
            => this.Status.IsOn(TaskEntryStatus.Completed);
        #endregion

        #region Public Methods
        public void Block()
        {
            if (!this.IsActive)
                throw new InvalidOperationException("Cannot block an inactive task.");

            var now = DateTime.UtcNow;

            this.Timeline.StatusChangedOn = now;
            this.Timeline.LastModifiedOn = now;

            this.Status.SetOn(TaskEntryStatus.Blocked);
        }

        public void Complete()
        {
            if (this.IsComplete)
                throw new InvalidOperationException("Cannot complete a completed task.");

            var now = DateTime.UtcNow;

            this.Timeline.CompletedOn = now;
            this.Timeline.StatusChangedOn = now;

            this.Status.SetTo(TaskEntryStatus.Completed);
        }

        public void SetStatusTo(TaskEntryStatus status)
        {
            if (this.Status.Is(status))
                return;

            var now = DateTime.UtcNow;
            var completing = status == TaskEntryStatus.Completed;
            var isComplete = this.IsComplete;

            if (status == TaskEntryStatus.Blocked)
            {
                this.Block();
                return;
            }

            if (completing && !isComplete)
            {
                this.Complete();
                return;
            }

            if (!completing && isComplete)
            {
                this.Timeline.CompletedOn = null;
            }

            this.Status.SetTo(status);
            this.Timeline.StatusChangedOn = now;
            this.Timeline.LastModifiedOn = now;
        }

        public void Unblock()
        {
            if (!this.IsBlocked)
                throw new InvalidOperationException("Cannot unblock an unblocked task.");

            var now = DateTime.UtcNow;

            this.Timeline.StatusChangedOn = now;
            this.Timeline.LastModifiedOn = now;

            this.Status.SetOff(TaskEntryStatus.Blocked);
        }
        #endregion

        #region Static Methods
        public static TaskEntry New(string title, TaskEntryStatus status)
        {
            var task = new TaskEntry
            {
                Title = title 
            };

            task.SetStatusTo(status);
            return task;
        }
        #endregion
    }
}
