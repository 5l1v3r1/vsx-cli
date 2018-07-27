using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;

namespace vsx.Commands
{
    [Command(Commands.Vsx,
        FullName = "vsx: vsts-cli tool",
        Description = "Search for VSTS build tasks in build and release definitions."),
        HelpOption,
        Subcommand(Commands.Connect, typeof(ConnectCommand)),
        Subcommand(Commands.Disconnect, typeof(DisconnectCommand)),
        Subcommand(Commands.Settings, typeof(SettingsCommand)),
        Subcommand(Commands.Builds, typeof(BuildDefinitionsCommand)),
        Subcommand(Commands.Releases, typeof(ReleaseDefinitionsCommand)),
        Subcommand(Commands.TaskGroups, typeof(TaskGroupsCommand))]
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
            _console.SpecifyASubcommand(Commands.Vsx);
            _app.ShowHelp();
            return 1;
        }
    }
}
