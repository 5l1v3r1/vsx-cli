using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using vsx.Models;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.List)]
    public class ListCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public ListCommand(CommandLineApplication app, IConsole console, IConnectionService connectionService)
            : base(console, app)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        [Argument(0)]
        public string Project { get; set; }

        private int OnExecute() => _connectionService.Connect(new CredentialsModel(AccountName, PersonalAccessToken)) ? GetResults().Result : ConnectionError();

        internal override async Task<int> GetTaskResults()
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
    }
}
