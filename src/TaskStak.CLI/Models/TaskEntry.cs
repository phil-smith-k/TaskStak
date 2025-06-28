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

        public bool IsOverdue
             => !this.IsCompleted && this.IsStaged && this.Timeline.StagedFor < DateOnly.FromDateTime(DateTime.Today);

        public bool IsStaged
            => !this.IsCompleted && this.Timeline.StagedFor.HasValue;
        #endregion

        #region Public Methods
        public void Block()
        {
            if (this.IsBlocked)
                throw new TaskStakException("Cannot block an already blocked task.");

            this.Timeline.LastModifiedOn = DateTime.UtcNow; 
            this.Status.SetTo(TaskEntryStatus.Blocked);
        }

        public void Complete()
        {
            if (this.IsCompleted)
                throw new TaskStakException("Cannot complete an already completed task.");

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

        public bool IsStagedFor(DateTime date)
            => this.IsStagedFor(DateOnly.FromDateTime(date));

        public bool IsStagedFor(DateOnly date)
            => this.Timeline.StagedFor.HasValue && this.Timeline.StagedFor == date;

        public void SetStatusTo(TaskEntryStatus status)
            => this.Status.SetTo(status);

        public void StageToStak(DateTime date)
        {
            this.StageToStak(DateOnly.FromDateTime(date));
        }

        public void StageToStak(DateOnly date)
        {
            if (date == DateOnly.MinValue)
                throw new ArgumentException("Invalid date provided to stage task to stak", nameof(date));

            this.Timeline.StagedFor = date;
            this.Timeline.LastModifiedOn = DateTime.UtcNow;
        }

        public void Unblock()
        {
            if (!this.IsBlocked)
                throw new TaskStakException("Cannot unblock an unblocked task.");

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
