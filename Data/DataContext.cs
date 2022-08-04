using AssetManager.Model;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Data;

public class DataContext: DbContext
{
    public DbSet<AssetModel> asset { get; }
    public DbSet<LoanAssetModel> loanAsset { get;}
    public DbSet<UserModel> usuario { get; }
    public DbSet<CompanyModel> company { get; }

    public DataContext(DbContextOptions<DataContext> options) :base(options)
    {
    }
    
    
}