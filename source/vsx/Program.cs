using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using vsx.Commands;
using vsx.Extensions;

namespace vsx
{
    public class Program
    {
        static int Main(string[] args)
        {
            var services = ConfigureApplicationServices();
            var app = ConfigureApplication(services);

            return app.Execute(args);
        }

        private static ServiceProvider ConfigureApplicationServices() 
            => new ServiceCollection().ConfigureServices()
                                      .BuildServiceProvider();

        private static CommandLineApplication<RootCommand> ConfigureApplication(ServiceProvider services)
        {
            var app = new CommandLineApplication<RootCommand>();

            app.Conventions
               .UseDefaultConventions()
               .UseConstructorInjection(services);

            return app;
        }
    }
}
