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
                       .AddSingleton<IFileService, FileService>()
                       .AddSingleton<ISettingsService, SettingsService>(sp => new SettingsService(sp.GetRequiredService<ICacheService>()))
                       .AddSingleton<IBuildDefinitionsService, BuildDefinitionsService>()
                       .AddSingleton<IReleaseDefinitionsService, ReleaseDefinitionsService>()
                       .AddSingleton<ITaskGroupsService, TaskGroupsService>(sp => new TaskGroupsService(sp.GetRequiredService<IConnectionService>(), sp.GetRequiredService<IParserService>()))
                       .AddSingleton<ITaskService, TaskService>(sp => new TaskService(sp.GetRequiredService<IConnectionService>(), sp.GetRequiredService<IParserService>()));
    }
}
