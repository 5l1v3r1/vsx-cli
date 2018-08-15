using McMaster.Extensions.CommandLineUtils;
using vsx.Extensions;
using VsxCommand = vsx.Helpers.Commands;

namespace vsx.Commands
{
    [Command(
        Name = VsxCommand.TaskGroups,
        Description = "Manage VSTS task groups' tasks."),
        HelpOption,
        Subcommand(VsxCommand.List, typeof(ListCommand)),
        Subcommand(VsxCommand.Search, typeof(SearchCommand)),
        Subcommand(VsxCommand.Details, typeof(DetailsCommand))]
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
            _console.SpecifyASubcommand(VsxCommand.TaskGroups);
            _app.ShowHelp();
            return 1;
        }
    }
}
