using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using vsx.Services;

namespace vsx.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection ConfigureServices(this ServiceCollection services)
            => services.AddSingleton<IConsole, PhysicalConsole>()
                       .AddSingleton<ICacheService, CacheService>()
                       .AddSingleton<IConnectionService, ConnectionService>(sp => new ConnectionService(sp.GetRequiredService<ICacheService>(), sp.GetRequiredService<IConsole>()))
                       .AddSingleton<IParserService, ParserService>()
                       .AddSingleton<ISettingsService, SettingsService>(sp => new SettingsService(sp.GetRequiredService<ICacheService>()))
                       .AddSingleton<IBuildDefinitionsService, BuildDefinitionsService>()
                       .AddSingleton<IReleaseDefinitionsService, ReleaseDefinitionsService>()
                       .AddSingleton<ITaskGroupsService, TaskGroupsService>()
                       .AddSingleton<ITaskService, TaskService>();
    }
}
