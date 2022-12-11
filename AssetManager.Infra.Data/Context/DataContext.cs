using AssetManager.Domain.Entities;
using AssetManager.Infra.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<AssetEntity> asset => Set<AssetEntity>();
        public DbSet<LoanAssetEntity> loanAsset => Set<LoanAssetEntity>();
        public DbSet<UserEntity> usuario => Set<UserEntity>();
        public DbSet<CompanyEntity> company => Set<CompanyEntity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
