using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.Search)]
    public class SearchCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public SearchCommand(
            CommandLineApplication app,
            IConsole console,
            IConnectionService connectionService)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        [Argument(0, "", "")]
        public int Project { get; set; }

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

        private int GetResults()
        {
            switch (_app.Parent.Name)
            {
                default:
                //case Commands.Tasks:
                //    return SearchTasks();
                case Commands.Builds:
                    return SearchBuilds().Result;
                //case Commands.Releases:
                //    return SearchReleases();
                //case Commands.TaskGroups:
                //    return SearchTaskGroups();
            }
        }

        private async Task<int> SearchBuilds()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var client = await _connectionService.GetBuildHttpClient();
            var definitions = await buildDefinitionsService.GetBuildDefinitions(client, "project");

            // parse results
            // output results

            return 1;
        }
    }
}
