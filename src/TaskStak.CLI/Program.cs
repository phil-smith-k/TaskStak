using System.CommandLine;
using TaskStak.CLI.Commands;

#if DEBUG
    Console.WriteLine("Waiting for debugger. Press any key to continue...");
    Console.ReadKey();
#endif

var rootCommand = new RootCommand("TaskStak - A developer-focused, performant task management CLI tool.")
{
    AddCommand.Create(),
};

return await rootCommand.InvokeAsync(args);
