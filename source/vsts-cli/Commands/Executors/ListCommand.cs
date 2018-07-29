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
            var buildDefinitionsService = _app.GetRequiredService<IBuildDefinitionsService>();
            var client = await _connectionService.GetBuildHttpClient();
            var definitions = await buildDefinitionsService.GetBuildDefinitions();

            // parse results

            if (true)
            {

            }

            // output results


            return 1;
        }
    }
}
