﻿using System.CommandLine;
using System.CommandLine.Parsing;
using TaskStak.CLI.Models;
using TaskStak.CLI.Presentation.Views;
using TaskStak.CLI.Utils;

namespace TaskStak.CLI.Commands
{
    public class ViewCommand : ITaskStakCommand
    {
        public static string Name => Constants.Commands.View;
        public static string Description => Constants.Commands.Descriptions.ViewDesc;

        public static Command Create()
        {
            var viewOption = new Option<ViewOption>(
                aliases: [Constants.Options.View, Constants.Options.ViewAlias], 
                parseArgument: ParseViewOption,
                isDefault: true,
                description: Constants.Options.Descriptions.ViewDesc);

            var command = new Command(Name, Description)
            {
                viewOption,
            };

            command.SetHandler(Execute, viewOption);

            return command;
        }

        public static void Execute(ViewOption viewArg)
        {
            var tasks = JsonHelper.LoadTasks();
            var options = new ListOptions
            {
                ViewOption = viewArg,
            };

            var view = ListViewFactory.GetViewFor(options);
            view.Render(tasks);
        }

        private static ViewOption ParseViewOption(ArgumentResult argResult)
        {
            ViewOption result = default;
            var arg = argResult.Tokens.SingleOrDefault()?.Value;

            if (string.IsNullOrWhiteSpace(arg))
                return result;

            var parsed = Enum.TryParse(arg, ignoreCase: true, out result);
            if (parsed)
                return result;

            result = arg.ToLowerInvariant() switch
            {
                "d" => ViewOption.Day,
                "w" => ViewOption.Week,
                "m" => ViewOption.Month,
                "v" => ViewOption.Verbose,

                _ => throw new TaskStakException($"Invalid option '{arg}'. Valid options: day (d), verbose (v)"),
            };

            return result;
        }
    }
}
