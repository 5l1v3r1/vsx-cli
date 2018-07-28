using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using vsx.Models;
using vsx.Services;

namespace vsx.Commands
{
    [Command(Name = Commands.Search)]
    public class SearchCommand : ExecutorBase
    {
        private readonly CommandLineApplication _app;
        private readonly IConsole _console;
        private readonly IConnectionService _connectionService;

        public SearchCommand(CommandLineApplication app, IConsole console, IConnectionService connectionService)
            : base(console, app)
        {
            _app = app;
            _console = console;
            _connectionService = connectionService;
        }

        [Argument(0, "", "")]
        public int Project { get; set; }

        private int OnExecute() => _connectionService.Connect(new CredentialsModel(AccountName, PersonalAccessToken)) ? GetResults().Result : ConnectionError();
    }
}
