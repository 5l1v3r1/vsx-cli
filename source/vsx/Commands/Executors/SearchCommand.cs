using System;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vsx.Services;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.Search,
        FullName = "",
        Description = "",
        ExtendedHelpText = "")]
    public class SearchCommand : ExecutorMethodsBase
    {
        private readonly CommandLineApplication _app;

        public SearchCommand(
            IConfiguration configuration,
            IConsole console,
            IConnectionService connectionService,
            IParserService parserService,
            IFileService fileService,
            CommandLineApplication app)
            : base(configuration, console, connectionService, parserService, fileService, app)
        {
            _app = app;
        }

        [Argument(
            order: 0,
            name: "Resource Id",
            description: "A valid id of a definition or a task to search for.")]
        public string ResourceId { get; set; }

        internal override async Task<int> GetTaskResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);
            var results = GetResultsModel(parsedId);

            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            results.TaskGroups = await taskGroupsService.SearchForTaskInTaskGroups(parsedId);

            var taskIdsToSearchFor = results.TaskGroups.Select(x => x.Id).ToList();
            taskIdsToSearchFor.Add(parsedId);

            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            results.BuildDefinitions = await buildDefinitionsService.SearchForTaskInBuildDefinitions(taskIdsToSearchFor);

            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            results.ReleaseDefinitions = await releaseDefinitionsService.SearchForTaskInReleaseDefinitions(taskIdsToSearchFor);

            return ProcessResults(results);
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
