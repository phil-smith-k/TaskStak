namespace TaskStak.CLI.Models
{
    public class TaskEntry : EntityRoot
    {
        #region Properties
        public Flags<TaskEntryStatus> Status { get; private set; } = Flags<TaskEntryStatus>.From(TaskEntryStatus.Active);

        public Timeline Timeline { get; set; } = Timeline.Begin();

        public required string Title { get; set; }

        public bool IsActive
            => this.Status.IsOn(TaskEntryStatus.Active);

        public bool IsBlocked
            => this.Status.IsOn(TaskEntryStatus.Blocked);

        public bool IsCompleted
            => this.Status.IsOn(TaskEntryStatus.Completed);
        #endregion

        #region Public Methods
        public void Block()
        {
            if (this.IsBlocked)
                throw new InvalidOperationException("Cannot block an already blocked task.");

            this.Timeline.LastModifiedOn = DateTime.UtcNow; 
            this.Status.SetTo(TaskEntryStatus.Blocked);
        }

        public void Complete()
        {
            if (this.IsCompleted)
                throw new InvalidOperationException("Cannot complete an already completed task.");

            this.Timeline.CompletedOn = DateTime.UtcNow;
            this.Status.SetTo(TaskEntryStatus.Completed);
        }

        public void EditStatus(TaskEntryStatus status)
        {
            if (this.Status.Is(status))
                return;

            var isComplete = this.IsCompleted;
            var completing = status == TaskEntryStatus.Completed && !isComplete;

            if (completing)
            {
                this.Complete();
                return;
            }

            if (!completing && isComplete)
            {
                this.Timeline.CompletedOn = null;
            }

            this.Timeline.LastModifiedOn = DateTime.UtcNow;
            this.Status.SetTo(status);
        }

        public void SetStatusTo(TaskEntryStatus status)
        {
            if (this.Status.Is(status))
                return;

            this.Status.SetTo(status);
        }   

        public void Unblock()
        {
            if (!this.IsBlocked)
                throw new InvalidOperationException("Cannot unblock an unblocked task.");

            this.Timeline.LastModifiedOn = DateTime.UtcNow;
            this.Status.SetTo(TaskEntryStatus.Active);
        }
        #endregion

        #region Static Methods
        public static TaskEntry New(string title, TaskEntryStatus status)
        {
            var task = new TaskEntry
            {
                Title = title 
            };

            task.EditStatus(status);
            return task;
        }
        #endregion
    }
}
