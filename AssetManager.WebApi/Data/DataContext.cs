using AssetManager.Model;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Data;

public class DataContext: DbContext
{
    public DbSet<AssetModel> asset { get; set;}
    public DbSet<LoanAssetModel> loanAsset { get; set;}
    public DbSet<UserModel> usuario { get; set;}
    public DbSet<CompanyModel> company { get; set; }

    public DataContext(DbContextOptions<DataContext> options) :base(options)
    {
    }
    
    
}