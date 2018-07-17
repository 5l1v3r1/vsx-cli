using McMaster.Extensions.CommandLineUtils;
using vsx.Commands;
using vsx.Extensions;

namespace vsx
{
    [Command(Name = "vsx", 
        FullName = "vsts-cli tool", 
        Description = "Manage VSTS build and release tasks."), 
        HelpOption,
        Subcommand("connect", typeof(ConnectCommand)),
        Subcommand("disconnect", typeof(DisconnectCommand)),
        Subcommand("options", typeof(OptionsCommand)),
        Subcommand("builds", typeof(BuildDefinitionsCommand)),
        Subcommand("releases", typeof(ReleaseDefinitionsCommand)),
        Subcommand("taskgroups", typeof(TaskGroupsCommand))]
    class Program
    {
        static int Main(string[] args)
        {
            var services = ServiceProviderExtensions.ConfigureServices();

            var app = new CommandLineApplication<Program>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            return app.Execute(args);
        }

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify at a subcommand.");
            app.ShowHelp();
            return 1;
        }
    }
}
