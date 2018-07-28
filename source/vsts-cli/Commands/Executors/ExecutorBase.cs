using McMaster.Extensions.CommandLineUtils;
using System;

namespace vsx.Commands
{
    public abstract class ExecutorBase
    {
        private readonly IConsole _console;

        public ExecutorBase(IConsole console)
        {
            _console = console;
        }

        [Option]
        public string AccountName { get; set; } = default;

        [Option]
        public string PersonalAccessToken { get; set; } = default;

        internal virtual int ConnectionError()
        {
            _console.ForegroundColor = ConsoleColor.Red;
            _console.WriteLine("Error during establishing connection.");
            return 0;
        }

        internal virtual int GetResults() => 0;
    }
}
