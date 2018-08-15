﻿using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using vsx.Services;

namespace vsx.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this ServiceCollection services)
            => services.AddSingleton<IConsole, PhysicalConsole>()
                       .AddSingleton<ICacheService, CacheService>()
                       .AddSingleton<IConnectionService, ConnectionService>()
                       .AddSingleton<IParserService, ParserService>()
                       .AddSingleton<IFileService, FileService>()
                       .AddSingleton<IBuildDefinitionsService, BuildDefinitionsService>()
                       .AddSingleton<IReleaseDefinitionsService, ReleaseDefinitionsService>()
                       .AddSingleton<ITaskGroupsService, TaskGroupsService>()
                       .AddSingleton<ITaskService, TaskService>();
    }
}
