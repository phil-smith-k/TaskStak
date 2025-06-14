using System.CommandLine;

namespace TaskStak.CLI.Commands
{
    public interface ITaskStakCommand
    {
        static abstract string Name { get; }

        static abstract string Description { get; }

        static abstract Command Create();
    }
}
