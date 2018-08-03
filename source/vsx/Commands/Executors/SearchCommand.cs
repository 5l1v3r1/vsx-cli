using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.Search)]
    public class SearchCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConnectionService _connectionService;

        public SearchCommand(
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

        [Argument(0)]
        public string TaskId { get; set; }

        private int OnExecute() => _connectionService.Connect(UseCredentialsFromOptions()) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetTaskResults()
        {
            var taskService = _app.GetRequiredService<ITaskService>();
            var task = await taskService.GetTaskById(TaskId);

            return ProcessResults(task);
        }

        internal override async Task<int> GetBuildResults()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var buildDefinition = await buildDefinitionsService.SearchForTaskInBuildDefinitions(TaskId);

            return ProcessResults(buildDefinition);
        }

        internal override async Task<int> GetReleaseResults()
        {
            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            var releaseDefinition = await releaseDefinitionsService.SearchForTaskInReleaseDefinitions(TaskId);

            return ProcessResults(releaseDefinition);
        }

        internal override async Task<int> GetTaskGroupResults()
        {
            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            var taskGroup = await taskGroupsService.SearchForTaskInTaskGroups(TaskId);

            return ProcessResults(taskGroup);
        }
    }
}
