using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssetManager.Infra.Data.Context;
using AssetManager.Infra.Data.Repository;
using AssetManager.Domain.Interfaces.Repositorys;
using Microsoft.Extensions.Configuration;
using AssetManager.Application.Profiles;
using MediatR;

namespace AssetManager.Infra.IoC
{
    public static class DependecyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<DataContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddAutoMapper(typeof(AssetProfile));
            services.AddAutoMapper(typeof(CompanyProfile));
            services.AddAutoMapper(typeof(LoanAssetProfile));
            services.AddAutoMapper(typeof(UserProfile));

            var myhandlers = AppDomain.CurrentDomain.Load("AssetManager.Application");
            services.AddMediatR(myhandlers);

            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<IAssetEventsRepository, AssetEventsRepository>();
            services.AddTransient<ILoanAssetRepository, LoanAssetRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
