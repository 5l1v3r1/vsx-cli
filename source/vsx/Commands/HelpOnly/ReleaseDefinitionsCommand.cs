using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.Releases,
        Description = "Manage VSTS release definition tasks."),
        HelpOption,
        Subcommand(VsxCommand.List, typeof(ListCommand)),
        Subcommand(VsxCommand.Search, typeof(SearchCommand)),
        Subcommand(VsxCommand.Details, typeof(DetailsCommand))]
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
            _console.SpecifyASubcommand(VsxCommand.Releases);
            _app.ShowHelp();
            return 1;
        }
    }
}
