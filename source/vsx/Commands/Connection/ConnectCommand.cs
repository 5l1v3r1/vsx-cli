using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;
using vsx.Models;
using vsx.Services;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.Connect,
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

        [Argument(order: 0, description: "The VSTS account name.", name: "vstsAccountName")]
        [Required]
        public string VstsAccountName { get; set; }

        [Argument(order: 1, description: "The VSTS project collection.", name: "project")]
        [Required]
        public string Project { get; set; }

        [Argument(order: 2, description: "A valid personal access token issued from VSTS.", name: "personalAccessToken")]
        [Required]
        public string PersonalAccessToken { get; set; }

        private int OnExecute()
        {
            var connection = _connectionService.Connect(new CredentialsModel(VstsAccountName, Project, PersonalAccessToken));

            if (connection)
            {
                _console.WriteLine($"Successful connection established to the account: {VstsAccountName}!");
                return 1;
            }

            _console.ErrorMessage("Error during connecting to VSTS!");
            return 0;
        }
    }
}
