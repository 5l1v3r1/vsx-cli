using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.Builds,
        Description = "Manage VSTS build definition tasks."),
        HelpOption,
        Subcommand(VsxCommand.List, typeof(ListCommand)),
        Subcommand(VsxCommand.Search, typeof(SearchCommand)),
        Subcommand(VsxCommand.Details, typeof(DetailsCommand))]
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
            _console.SpecifyASubcommand(VsxCommand.Builds);
            _app.ShowHelp();
            return 1;
        }
    }
}
