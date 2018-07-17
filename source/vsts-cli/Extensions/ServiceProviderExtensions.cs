using Microsoft.Extensions.DependencyInjection;
using vsx.Services;

namespace vsx.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static ServiceProvider ConfigureServices()
            => new ServiceCollection()
                .AddSingleton<IConnectionService, ConnectionService>()
                .BuildServiceProvider();
    }
}
