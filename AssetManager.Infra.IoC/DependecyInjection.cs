using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssetManager.Infra.Data.Context;
using AssetManager.Infra.Data.Repository;

namespace AssetManager.Infra.IoC
{
    public static class DependecyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<DataContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddTransient<AssetRepository>();
        }
    }
}
