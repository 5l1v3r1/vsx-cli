using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(Name = VsxCommand.Tasks,
        Description = "Manage tasks."),
        HelpOption,
        Subcommand(VsxCommand.List, typeof(ListCommand)),
        Subcommand(VsxCommand.Search, typeof(SearchCommand)),
        Subcommand(VsxCommand.Details, typeof(DetailsCommand))]
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
            _console.SpecifyASubcommand(VsxCommand.TaskGroups);
            _app.ShowHelp();
            return 1;
        }
    }
}
