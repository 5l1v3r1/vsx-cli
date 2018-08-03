using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;

namespace vsx.Commands
{
    [Command(Name = Commands.Tasks,
        Description = "Manage tasks."),
        HelpOption,
        Subcommand(Commands.List, typeof(ListCommand)),
        Subcommand(Commands.Search, typeof(SearchCommand)),
        Subcommand(Commands.Details, typeof(DetailsCommand))]
    public class TasksCommand
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public TasksCommand(IConsole console, CommandLineApplication app)
        {
            _console = console;
            _app = app;
        }

        private int OnExecute()
        {
            _console.SpecifyASubcommand(Commands.TaskGroups);
            _app.ShowHelp();
            return 1;
        }
    }
}
