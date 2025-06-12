using System.CommandLine;
using TaskStak.CLI.Commands;

var rootCommand = new RootCommand("TaskStak - A brutally fast task management CLI")
{
    AddCommand.Create() 
};

return await rootCommand.InvokeAsync(args);
