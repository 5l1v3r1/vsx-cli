using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vsx.Services;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.Details, 
        Description = "",
        ExtendedHelpText = "")]
    public class DetailsCommand : ExecutorMethodsBase
    {
        private readonly CommandLineApplication _app;

        public DetailsCommand(
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

        [Argument(0)]
        public string ResourceId { get; set; }

        internal override async Task<int> GetTaskResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);

            var taskService = _app.GetRequiredService<ITaskService>();
            var task = await taskService.GetTaskById(parsedId);

            return ProcessResults(task);
        }

        internal override async Task<int> GetBuildResults()
        {
            var parsedId = ParsePredicate<Int32>(ResourceId);

            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var buildDefinition = await buildDefinitionsService.GetBuildDefinitionById(parsedId);

            return ProcessResults(buildDefinition);
        }

        internal override async Task<int> GetReleaseResults()
        {
            var parsedId = ParsePredicate<Int32>(ResourceId);

            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            var releaseDefinition = await releaseDefinitionsService.GetReleaseDefinitionById(parsedId);

            return ProcessResults(releaseDefinition);
        }

        internal override async Task<int> GetTaskGroupResults()
        {
            var parsedId = ParsePredicate<Guid>(ResourceId);

            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            var taskGroup = await taskGroupsService.GetTaskGroupById(parsedId);

            return ProcessResults(taskGroup);
        }
    }
}
