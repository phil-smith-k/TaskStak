namespace TaskStak.CLI.Models
{
    public class SearchResult<T> where T : EntityRoot
    {
        public T? Entity { get; init; }

        public List<T> Candidates { get; init; } = [];

        public bool CandidatesFound 
            => this.Candidates.Count > 0;

        public bool EntityFound 
            => this.Entity != null && !this.CandidatesFound;

        public bool NoResults 
            => this.Entity == null && this.Candidates.Count == 0;

        public T GetEntity()
            => this.Entity ?? throw new InvalidOperationException("No entity found in search result.");
    }
}
