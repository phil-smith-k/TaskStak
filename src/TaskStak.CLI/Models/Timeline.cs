namespace TaskStak.CLI.Models
{
    public class Timeline
    {
        private Timeline()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        #region Properties
        public DateTime? CompletedOn { get; private set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }
        #endregion

        #region Public Methods
        public void End()
        {
            this.CompletedOn = DateTime.UtcNow;
            this.LastModifiedOn = DateTime.UtcNow;
        }
        #endregion

        #region Static Methods
        public static Timeline Begin()
        {
            return new Timeline();
        }
        #endregion
    }
}
