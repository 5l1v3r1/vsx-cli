using System;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
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
        public string ResourceId { get; set; }

        private int OnExecute() => _connectionService.Connect(UseCredentialsFromOptions()) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetTaskResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);

            // Search in task groups
            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            var taskGroups = await taskGroupsService.SearchForTaskInTaskGroups(parsedId);

            // get task group ids
            var taskGroupIds = taskGroups.Select(x => x.Id).ToList();


            // search in builds for task and task groups result
            // search in releases for task and task groups result

            return 1;
        }

        internal override async Task<int> GetBuildResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);

            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var buildDefinitions = await buildDefinitionsService.SearchForTaskInBuildDefinitions(parsedId);

            return ProcessResults(buildDefinitions);
        }

        internal override async Task<int> GetReleaseResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);

            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            var releaseDefinitions = await releaseDefinitionsService.SearchForTaskInReleaseDefinitions(parsedId);

            return ProcessResults(releaseDefinitions);
        }

        internal override async Task<int> GetTaskGroupResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);

            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            var taskGroups = await taskGroupsService.SearchForTaskInTaskGroups(parsedId);

            return ProcessResults(taskGroups);
        }
    }
}
