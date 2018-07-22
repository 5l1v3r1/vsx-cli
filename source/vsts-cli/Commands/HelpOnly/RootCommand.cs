using McMaster.Extensions.CommandLineUtils;

namespace vsx.Commands
{
    [Command(Commands.Vsx,
        FullName = "vsx-cli tool",
        Description = "Manage VSTS build and release tasks."),
        HelpOption,
        Subcommand(Commands.Connect, typeof(ConnectCommand)),
        Subcommand(Commands.Disconnect, typeof(DisconnectCommand)),
        Subcommand(Commands.Context, typeof(ContextCommand)),
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

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            _console.WriteLine("You must specify at a subcommand.");
            _app.ShowHelp();
            return 1;
        }
    }
}
