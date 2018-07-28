using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;

namespace vsx.Commands
{
    [Command(Name = Commands.Builds,
        Description = "Manage VSTS build definition tasks."),
        HelpOption,
        Subcommand(Commands.List, typeof(ListCommand)),
        Subcommand(Commands.Search, typeof(SearchCommand)),
        Subcommand(Commands.Details, typeof(DetailsCommand))]
    public class BuildDefinitionsCommand
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public BuildDefinitionsCommand(IConsole console, CommandLineApplication app)
        {
            _console = console;
            _app = app;
        }

        private int OnExecute()
        {
            _console.SpecifyASubcommand(Commands.Builds);
            _app.ShowHelp();
            return 1;
        }
    }
}
