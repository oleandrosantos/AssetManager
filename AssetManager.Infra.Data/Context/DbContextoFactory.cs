using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AssetManager.Infra.Data.Context;
public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AssetManager.WebApi"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        string conection = config.GetConnectionString("DefaultConnection");
        ServerVersion serverVersion = ServerVersion.AutoDetect(conection);

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseMySql(conection, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        return new DataContext(optionsBuilder.Options);
    }
}
