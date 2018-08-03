using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.List)]
    public class ListCommand : ExecutorBase
    {
        private readonly IConnectionService _connectionService;
        private readonly CommandLineApplication _app;

        public ListCommand(
            IConsole console,
            IConnectionService connectionService,
            IParserService parserService,
            IFileService fileService,
            CommandLineApplication app)
            : base(console, parserService, fileService, app)
        {
            _app = app;
            _connectionService = connectionService;
        }

        private int OnExecute() => _connectionService.Connect(UseCredentialsFromOptions()) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetTaskResults()
        {
            var taskService = _app.GetRequiredService<ITaskService>();
            var tasks = await taskService.GetTasks();

            return ProcessResults(tasks);
        }

        internal override async Task<int> GetBuildResults()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var buildDefinitions = await buildDefinitionsService.GetBuildDefinitions();

            return ProcessResults(buildDefinitions);
        }

        internal override async Task<int> GetReleaseResults()
        {
            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            var releaseDefinitions = await releaseDefinitionsService.GetReleaseDefinitions();

            return ProcessResults(releaseDefinitions);
        }

        internal override async Task<int> GetTaskGroupResults()
        {
            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            var taskGroups = await taskGroupsService.GetTaskGroups();

            return ProcessResults(taskGroups);
        }
    }
}
