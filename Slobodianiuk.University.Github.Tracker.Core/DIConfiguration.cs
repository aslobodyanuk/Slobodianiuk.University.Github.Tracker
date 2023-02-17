using AutoMapper;
using Finance.Formatter.Core.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slobodianiuk.University.Github.Tracker.Core.Interfaces;
using Slobodianiuk.University.Github.Tracker.Core.Services;
using Slobodianiuk.University.Github.Tracker.Core.Services.Frontend;
using Slobodianiuk.University.Github.Tracker.Models.Configuration;

namespace Slobodianiuk.University.Github.Tracker.Core
{
    public static class DIConfiguration
    {
        public static void RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddScoped<IGithubCommitSynchronizationService, GithubCommitSynchronizationService>();
            services.AddScoped<GithubService>();
            services.AddScoped<IGithubService, GithubService>();
            services.AddScoped<ITrackerFrontendService, TrackerFrontendService>();

            services.AddSingleton<IMapperProvider, MapperProvider>();
            services.AddSingleton(x => GetMapper(x));
        }

        public static void RegisterCoreConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
        }

        private static IMapper GetMapper(IServiceProvider serviceProvider)
        {
            var provider = serviceProvider.GetRequiredService<IMapperProvider>();
            return provider.GetMapper();
        }
    }
}
