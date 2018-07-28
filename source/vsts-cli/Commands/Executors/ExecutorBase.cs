using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace vsx.Commands
{
    public abstract class ExecutorBase
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public ExecutorBase(IConsole console, CommandLineApplication app)
        {
            _console = console;
            _app = app;
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

        internal virtual async Task<int> GetResults()
        {
            switch (_app.Parent.Name)
            {
                default:
                case Commands.Tasks:
                    return await GetTaskResults();
                case Commands.Builds:
                    return await GetBuildResults();
                case Commands.Releases:
                    return await GetReleaseResults();
                case Commands.TaskGroups:
                    return await GetTaskGroupResults();
            }
        }

        internal virtual async Task<int> GetTaskResults() => 0;

        internal virtual async Task<int> GetBuildResults() => 0;

        internal virtual async Task<int> GetReleaseResults() => 0;

        internal virtual async Task<int> GetTaskGroupResults() => 0;
    }
}
