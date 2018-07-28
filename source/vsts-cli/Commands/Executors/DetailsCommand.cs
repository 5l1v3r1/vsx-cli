using McMaster.Extensions.CommandLineUtils;
using System;
using vsx.Services;

namespace vsx.Commands.Executors
{
    [Command(Name = Commands.List)]
    public class DetailsCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public DetailsCommand(CommandLineApplication app, IConsole console, IConnectionService connectionService)
            : base(console)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        private int OnExecute() => (_connectionService.Connect(AccountName, PersonalAccessToken)) ? GetResults() : ConnectionError();

        internal override int GetResults()
        {
            return 1;
        }
    }
}
