using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using vsx.Commands;
using vsx.Extensions;

namespace vsx
{
    public class Program
    {
        static int Main(string[] args)
        {
            var configuration = Configure();
            var services = ConfigureServices(configuration);
            var app = ConfigureCommandLineApplication(services);

            return app.Execute(args);
        }

        private static IConfiguration Configure()
            => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                         .Build();

        private static ServiceProvider ConfigureServices(IConfiguration configuration) 
            => new ServiceCollection().ConfigureApplicationServices()
                                      .AddSingleton(configuration)
                                      .BuildServiceProvider();

        private static CommandLineApplication<RootCommand> ConfigureCommandLineApplication(ServiceProvider services)
        {
            var app = new CommandLineApplication<RootCommand>();

            app.Conventions
               .UseDefaultConventions()
               .UseConstructorInjection(services);

            return app;
        }
    }
}
