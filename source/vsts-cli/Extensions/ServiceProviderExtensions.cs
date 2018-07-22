using Microsoft.Extensions.DependencyInjection;
using vsx.Services;

namespace vsx.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection ConfigureServices(this ServiceCollection services)
            => services.AddSingleton<IConnectionService, ConnectionService>()
                       .AddSingleton<IBuildDefinitionsService, BuildDefinitionsService>()
                       .AddSingleton<IReleaseDefinitionsService, ReleaseDefinitionsService>()
                       .AddSingleton<ITaskGroupsService, TaskGroupsService>()
                       .AddSingleton<ITaskService, TaskService>()
                       .AddSingleton<IContextService, ContextService>()
                       .AddSingleton<IOutputService, OutputService>();
    }
}
