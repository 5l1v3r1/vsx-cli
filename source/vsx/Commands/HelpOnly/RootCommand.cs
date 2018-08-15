using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.Vsx,
        FullName = "vsx: vsts-cli tool",
        Description = "Search for VSTS build tasks in build and release definitions."),
        HelpOption,
        Subcommand(VsxCommand.Connect, typeof(ConnectCommand)),
        Subcommand(VsxCommand.Disconnect, typeof(DisconnectCommand)),
        Subcommand(VsxCommand.Builds, typeof(BuildDefinitionsCommand)),
        Subcommand(VsxCommand.Releases, typeof(ReleaseDefinitionsCommand)),
        Subcommand(VsxCommand.TaskGroups, typeof(TaskGroupsCommand)),
        Subcommand(VsxCommand.Tasks, typeof(TasksCommand))]
    public class RootCommand
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public RootCommand(IConsole console, CommandLineApplication app)
        {
            _console = console;
            _app = app;
        }

        private int OnExecute()
        {
            _console.SpecifyASubcommand(VsxCommand.Vsx);
            _app.ShowHelp();
            return 1;
        }
    }
}
