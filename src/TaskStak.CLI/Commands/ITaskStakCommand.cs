using System.CommandLine;

namespace TaskStak.CLI.Commands
{
    public interface ITaskStakCommand
    {
        static string Name => string.Empty;

        static string? Description { get; }

        static Command Create => new(Name, Description);
    }
}
