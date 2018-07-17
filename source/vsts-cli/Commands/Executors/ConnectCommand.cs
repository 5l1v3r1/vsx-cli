using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = "connect")]
    public class ConnectCommand
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public ConnectCommand(CommandLineApplication app, IConsole console, IConnectionService connectionService)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        [Option(Description = "The valid url of the VSTS service collection.", 
            LongName = "--serviceCollectionUrl", 
            ShortName = "-sc")]
        public string ServiceCollectionUrl { get; set; }

        [Option(Description = "A valid personal access token issued from VSTS.",
            LongName = "--personalAccessToken",
            ShortName = "-pat")]
        public string PersonalAccessToken { get; set; }

        private int OnExecute()
        {
            _connectionService.Connect(serviceCollectionUrl: ServiceCollectionUrl, personalAccessToken: PersonalAccessToken);


            _console.WriteLine("You must specify at a subcommand.");
            _app.ShowHelp();
            return 1;
        }
    }
}
