namespace TaskStak.CLI.Models
{
    public class Timeline
    {
        public Timeline()
        { }

        #region Properties
        public DateTimeOffset? CompletedOn { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset? LastModifiedOn { get; set; }

        public DateOnly? StagedFor { get; set; }
        #endregion

        #region Static Methods
        public static Timeline Begin()
        {
            return new Timeline
            {
                CreatedOn = DateTimeOffset.Now,
            };
        }
        #endregion
    }
}
