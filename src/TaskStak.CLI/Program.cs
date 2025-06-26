using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using TaskStak.CLI;
using TaskStak.CLI.Commands;
using TaskStak.CLI.Utils;

Startup.SetupInterface();

#if DEBUG
    Console.WriteLine("Pausing execution to allow debugger to be attached. Press any key to continue...");
    Console.ReadKey();
#endif

var rootCommand = new RootCommand("TaskStak - A developer-focused, performant task management CLI tool.")
{
    AddCommand.Create(),
    DoneCommand.Create(),
    MoveCommand.Create(),
    StakCommand.Create(),
    TitleCommand.Create(),
    ViewCommand.Create(),
};

var commandBuilder = new CommandLineBuilder(rootCommand)
    .UseDefaults()
    .UseExceptionHandler(async (exception, context) =>
    {
        await Task.Run(() =>
        {
            var exitCode = ExceptionHandler.HandleGlobalException(exception);
            context.ExitCode = exitCode;
        });
    });

return await commandBuilder
    .Build()
    .InvokeAsync(args);
