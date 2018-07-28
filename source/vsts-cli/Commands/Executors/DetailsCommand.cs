using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
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

        public DetailsCommand(CommandLineApplication app, IConsole console, IConnectionService connectionService)
            : base(console, app)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        [Argument(0)]
        public string Project { get; set; }

        [Argument(1)]
        public int BuildId { get; set; }

        private int OnExecute() => _connectionService.Connect(new CredentialsModel(AccountName, PersonalAccessToken)) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetBuildResults()
        {
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var client = await _connectionService.GetBuildHttpClient();
            var definition = await buildDefinitionsService.GetBuildDefinitionById(client, Project, BuildId);

            _console.WriteLine(definition);

            return 1;
        }
    }
}
