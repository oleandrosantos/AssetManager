using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.Repository;
public class AtivosRepository : RepositoryBase<AtivoEntity>, IAtivosRepository
{
    public AtivosRepository(DataContext dbContext) : base(dbContext) { }

    public async Task<IList<AtivoEntity>> ObterAtivosDaCompanhia(int IdCompanhia)
    {
        return dbSet.Where(a => a.IdCompanhia == IdCompanhia).ToList();
    }

    public Task<AtivoEntity?> ObterAtivoPorSKU(string SKU)
    {
       return dbSet.Where(a => a.Sku == SKU).FirstOrDefaultAsync();
    }

    public Task Delete(int id, string? ExclusionInfo)
    {
        var asset = ObterAtivoPorId(id).Result;
        asset.DataExclusao = DateTime.Now;
        asset.InformacoesExclusao = ExclusionInfo;
        context.SaveChangesAsync();
        return Task.CompletedTask;
    }

    public async Task<AtivoEntity?> ObterAtivoPorId(int id)
    {
        var asset = dbSet.Where(a => a.IdAtivo == id)
                         .Include(a => a.EventosAtivo)                   
                         .FirstOrDefault();

        return asset;
    }
}
