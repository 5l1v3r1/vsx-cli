using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.Connect,
        FullName = "vsx: connect command",
        Description = "Establish a connection to your VSTS account"),
        HelpOption]
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
        [Required]
        public string VstsAccountName { get; set; }

        [Option("-pat|--personalAccessToken", Description = "A valid personal access token issued from VSTS.")]
        [Required]
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
