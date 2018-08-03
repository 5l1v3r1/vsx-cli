using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.List)]
    public class DetailsCommand : ExecutorBase
    {
        private readonly IConnectionService _connectionService;
        private readonly CommandLineApplication _app;

        public DetailsCommand(
            IConsole console,
            IConnectionService connectionService,
            IParserService parserService,
            IFileService fileService,
            CommandLineApplication app)
            : base(console, parserService, fileService, app)
        {
            _connectionService = connectionService;
            _app = app;
        }

        [Argument(0)]
        public string DefinitionId { get; set; }

        [Option(CommandOptionType.NoValue)]
        public bool Detailed { get; set; }

        private int OnExecute() => _connectionService.Connect(UseCredentialsFromOptions()) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetTaskResults()
        {
            var taskService = _app.GetRequiredService<ITaskService>();
            var task = await taskService.GetTaskById(DefinitionId);

            return ProcessResults(task);
        }

        internal override async Task<int> GetBuildResults()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var buildDefinition = await buildDefinitionsService.GetBuildDefinitionById(DefinitionId);

            return ProcessResults(buildDefinition);
        }

        internal override async Task<int> GetReleaseResults()
        {
            var releaseDefinitionsService = _app.GetRequiredService<IReleaseDefinitionsService>();
            var releaseDefinition = await releaseDefinitionsService.GetReleaseDefinitionById(DefinitionId);

            return ProcessResults(releaseDefinition);
        }

        internal override async Task<int> GetTaskGroupResults()
        {
            var taskGroupsService = _app.GetRequiredService<ITaskGroupsService>();
            var taskGroup = await taskGroupsService.GetTaskGroupById(DefinitionId);

            return ProcessResults(taskGroup);
        }
    }
}
