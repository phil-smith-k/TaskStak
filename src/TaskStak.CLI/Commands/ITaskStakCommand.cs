using System.CommandLine;

namespace TaskStak.CLI.Commands
{
    public interface ITaskStakCommand
    {
        static abstract Command Create();
    }
}
