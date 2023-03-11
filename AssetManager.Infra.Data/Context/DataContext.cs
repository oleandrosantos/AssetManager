using AssetManager.Domain.Entities;
using AssetManager.Infra.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<AtivoEntity> ativo => Set<AtivoEntity>();
        public DbSet<UsuarioEntity> usuario => Set<UsuarioEntity>();
        public DbSet<EventosAtivoEntity> company => Set<EventosAtivoEntity>();
        public DbSet<EventosAtivoEntity> assetEvents => Set<EventosAtivoEntity>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AssetConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new AssetEventsConfiguration());
        }
    }
}
