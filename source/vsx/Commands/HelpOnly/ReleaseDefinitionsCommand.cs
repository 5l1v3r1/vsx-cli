using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;

namespace vsx.Commands
{
    [Command(Name = Commands.Releases,
        Description = "Manage VSTS release definition tasks."),
        HelpOption,
        Subcommand(Commands.List, typeof(ListCommand)),
        Subcommand(Commands.Search, typeof(SearchCommand)),
        Subcommand(Commands.Details, typeof(DetailsCommand))]
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
            _console.SpecifyASubcommand(Commands.Releases);
            _app.ShowHelp();
            return 1;
        }
    }
}
