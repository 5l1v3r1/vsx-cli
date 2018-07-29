using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;

namespace vsx.Commands
{
    [Command(Name = Commands.TaskGroups,
        Description = "Manage VSTS task groups' tasks."),
        HelpOption,
        Subcommand(Commands.List, typeof(ListCommand)),
        Subcommand(Commands.Search, typeof(SearchCommand)),
        Subcommand(Commands.Details, typeof(DetailsCommand))]
    public class TaskGroupsCommand
    {
        private readonly IConsole _console;
        private readonly CommandLineApplication _app;

        public TaskGroupsCommand(IConsole console, CommandLineApplication app)
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
