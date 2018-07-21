using McMaster.Extensions.CommandLineUtils;
using System;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = "connect")]
    public class ConnectCommand
    {
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public ConnectCommand(IConsole console, IConnectionService connectionService)
        {
            _console = console;
            _connectionService = connectionService;
        }

        [Option("-acc|--accountName", Description = "The VSTS account name.")]
        public string VstsAccountName { get; set; }

        [Option("-pat|--personalAccessToken", Description = "A valid personal access token issued from VSTS.")]
        public string PersonalAccessToken { get; set; }

        private int OnExecute()
        {
            var connection = _connectionService.Connect(vstsAccountName: VstsAccountName, personalAccessToken: PersonalAccessToken);

            if (connection)
            {
                _console.WriteLine($"Successful connection established!");
                return 1;
            }

            _console.ForegroundColor = ConsoleColor.Red;
            _console.WriteLine("Error during establishing connection.");
            return 0;
        }
    }
}
