namespace TaskStak.CLI.Presentation.Sections
{
    public interface ISectionView
    {
        string Title { get; }

        void Render();

        string GetHeader();

        void NoContent();
    }
}
