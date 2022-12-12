using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.Repository;
public class AssetRepository : RepositoryBase<AssetEntity>, IAssetRepository
{
    public AssetRepository(DataContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<AssetEntity>> AssetsByCompanyList(int idCompany)
    {
        return context.asset.Where(a => a.IdCompany == idCompany).ToList();
    }

    public Task<AssetEntity?> GetBySKU(string SKU)
    {
       return context.asset.Where(a => a.Sku == SKU).FirstOrDefaultAsync();
    }
}
