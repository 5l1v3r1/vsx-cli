using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.List)]
    public class ListCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public ListCommand(
            CommandLineApplication app, 
            IConsole console, 
            IConnectionService connectionService)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        [Argument(0)]
        public string Project { get; set; }

        private int OnExecute()
        {
            if (_connectionService.CheckConnection())
            {
                return GetResults();
            }
            else
            {
                var x =  _app.Parent;
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
                case Commands.Tasks:
                    return ListTasks();
                case Commands.Builds:
                    return ListBuilds().Result;
                case Commands.Releases:
                    return ListReleases();
                case Commands.TaskGroups:
                    return ListTaskGroups();
            }
        }

        private async Task<int> ListBuilds()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var client = await _connectionService.GetBuildHttpClient();
            var definitions = await buildDefinitionsService.GetBuildDefinitions(client, Project);

            // parse results

            if (true)
            {

            }
            
            // output results
            

            return 1;
        }

        private int ListReleases()
        {
            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            //var client = await _connectionService.GetBuildHttpClient();
            //var definitions = await buildDefinitionsService.GetBuildDefinitions(client);


            return 1;
        }

        private int ListTaskGroups()
        {
            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();


            return 1;
        }

        private int ListTasks()
        {
            var taskService = _app.GetRequiredService<ITaskService>();

            return 1;
        }
    }
}
