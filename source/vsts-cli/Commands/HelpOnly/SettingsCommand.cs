using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;

namespace vsx.Commands
{
    [Command(Name = Commands.Settings,
        Description = "Manage the settings for the application."),
        HelpOption,
        Subcommand(Commands.Get, typeof(GetCommand)),
        Subcommand(Commands.Set, typeof(SetCommand))]
    public class SettingsCommand
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public SettingsCommand(IConsole console, CommandLineApplication app)
        {
            _console = console;
            _app = app;
        }

        private int OnExecute()
        {
            _console.SpecifyASubcommand(Commands.Settings);
            _app.ShowHelp();
            return 1;
        }
    }
}
