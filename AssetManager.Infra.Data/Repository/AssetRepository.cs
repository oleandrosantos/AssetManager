﻿using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
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

    public async Task<IList<AssetEntity>> GetAssetsByCompany(int idCompany)
    {
        return dbSet.Where(a => a.IdCompany == idCompany).ToList();
    }

    public Task<AssetEntity?> GetBySKU(string SKU)
    {
       return dbSet.Where(a => a.Sku == SKU).FirstOrDefaultAsync();
    }

    public Task Delete(int id, string? ExclusionInfo)
    {
        var asset = GetById(id).Result;
        asset.ExclusionDate = DateTime.Now;
        asset.ExclusionInfos = ExclusionInfo;
        context.SaveChangesAsync();
        return Task.CompletedTask;
    }

    public async Task<AssetEntity?> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }
}
