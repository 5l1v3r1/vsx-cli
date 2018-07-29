using McMaster.Extensions.CommandLineUtils;
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

        private int OnExecute() => _connectionService.Connect(UseCredentialsFromOptions()) ? GetResults().Result : ConnectionError();
    }
}
