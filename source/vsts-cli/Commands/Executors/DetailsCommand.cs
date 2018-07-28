using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using vsx.Extensions;
using vsx.Models;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.List)]
    public class DetailsCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;
        private readonly IParserService _parseService;
        private readonly IFileService _fileService;

        public DetailsCommand(
            CommandLineApplication app, 
            IConsole console, IConnectionService connectionService, 
            IParserService parserService, 
            IFileService fileService)
            : base(console, app)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
            _parseService = parserService;
            _fileService = fileService;
        }

        [Argument(0)]
        public string Project { get; set; }

        [Argument(1)]
        public int BuildId { get; set; }

        [Option(CommandOptionType.NoValue)]
        public bool Detailed { get; set; }

        private int OnExecute() => _connectionService.Connect(new CredentialsModel(AccountName, PersonalAccessToken)) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetBuildResults()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var client = await _connectionService.GetBuildHttpClient();
            var definition = await buildDefinitionsService.GetBuildDefinitionById(client, Project, BuildId);

            var parsedResults = _parseService.SerializeBuildDetails(definition);
            _fileService.SaveToJson(parsedResults);
            _console.WriteLine(parsedResults);

            return 1;
        }
    }
}
