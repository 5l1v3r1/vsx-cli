using McMaster.Extensions.CommandLineUtils;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.Disconnect,
        FullName = "vsx: disconnect command",
        Description = "Clear the connection cache."),
        HelpOption]
    public class DisconnectCommand
    {
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public DisconnectCommand(IConsole console, IConnectionService connectionService)
        {
            _console = console;
            _connectionService = connectionService;
        }

        private int OnExecute()
        {
            _connectionService.Disconnect();

            _console.WriteLine("Disconnected.");
            return 1;
        }
    }
}
