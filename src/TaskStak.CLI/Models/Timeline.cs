namespace TaskStak.CLI.Models
{
    public class Timeline
    {
        public Timeline()
        { }

        #region Properties
        public DateTime? CompletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateOnly? StagedFor { get; set; }
        #endregion

        #region Static Methods
        public static Timeline Begin()
        {
            return new Timeline
            {
                CreatedOn = DateTime.UtcNow,
            };
        }
        #endregion
    }
}
