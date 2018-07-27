using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using vsx.Services;

namespace vsx.Commands.Executors
{
    [Command(Name = Commands.List)]
    public class DetailsCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public DetailsCommand(
            CommandLineApplication app,
            IConsole console,
            IConnectionService connectionService)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        private int OnExecute()
        {
            if (_connectionService.CheckConnection())
            {
                return GetResults();
            }
            else
            {
                var x = _app.Parent;
            }

            _console.ForegroundColor = ConsoleColor.Red;
            _console.WriteLine("Error during establishing connection.");
            return 0;
        }
    }
}
