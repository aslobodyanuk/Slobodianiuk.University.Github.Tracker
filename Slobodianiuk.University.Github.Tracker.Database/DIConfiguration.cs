using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Slobodianiuk.University.Github.Tracker.Database
{
    public static class DIConfiguration
    {
        public static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<TrackerDbContext>((x) => x.UseSqlServer(configuration.GetConnectionString("TrackerDatabase")));
        }
    }
}
