using System.CommandLine;
using TaskStak.CLI;
using TaskStak.CLI.Commands;

Startup.SetupInterface();

#if DEBUG
    Console.WriteLine("Pausing execution to allow debugger to be attached. Press any key to continue...");
    Console.ReadKey();
#endif

var rootCommand = new RootCommand("TaskStak - A developer-focused, performant task management CLI tool.")
{
    AddCommand.Create(),
    DoneCommand.Create(),
    TitleCommand.Create(),
    ViewCommand.Create(),
};

return await rootCommand.InvokeAsync(args);