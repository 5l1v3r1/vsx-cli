using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vsx.Services;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.List,
        Description = "List tasks, task groups, build and release definitions.",
        ExtendedHelpText = "")]
    public class ListCommand : ExecutorMethodsBase
    {
        private readonly CommandLineApplication _app;

        public ListCommand(
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
