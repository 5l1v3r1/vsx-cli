﻿using McMaster.Extensions.CommandLineUtils;

namespace vsx.Commands
{
    [Command(Name = Commands.Releases,
        Description = "Manage VSTS release definition tasks."),
        HelpOption,
        Subcommand(Commands.List, typeof(ListCommand)),
        Subcommand(Commands.Search, typeof(SearchCommand))]
    public class ReleaseDefinitionsCommand
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public ReleaseDefinitionsCommand(IConsole console, CommandLineApplication app)
        {
            _console = console;
            _app = app;
        }

        private int OnExecute()
        {
            _console.WriteLine("You must specify at a subcommand.");
            _app.ShowHelp();
            return 1;
        }
    }
}
