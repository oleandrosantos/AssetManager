using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;

namespace AssetManager.Infra.Data.Repository;
public class AssetEventsRepository : RepositoryBase<AssetEventsEntity>, IAssetEventsRepository
{
	public AssetEventsRepository(DataContext dbContext) : base(dbContext) { }

    public Task<AssetEventsEntity?> GetByID(int id)
    {
        var assetEvent = dbSet.Where(a => a.IdEvent == id).FirstOrDefault();
        return Task.FromResult(assetEvent);
    }
    public Task LoanAsset(AssetEventsEntity assetEvent)
    {
        context.assetEvents.Add(assetEvent);
        context.SaveChanges();
        return Task.FromResult(assetEvent.IdAsset);
    }

    public async Task<IList<AssetEventsEntity>> GetAllByAsset(int idAsset)
    {
        var events = dbSet
            .Where(a => a.IdAsset == idAsset)
            .OrderBy(a => a.EventDate)
            .ToList();

        return events;
    }

    public async Task<IList<AssetEventsEntity>?> GetLoansAssetsByAssetId(int idAsset)
    {
        var AssetList = dbSet.Where(a => a.Asset.IdAsset == idAsset).ToList();
        return AssetList;
    }

    public async Task<IList<AssetEventsEntity>?> GetLoansAssetsByCompanyId(int idCompany)
    {
        var AssetList = dbSet.Where(a => a.Asset.Company.IdCompany == idCompany).ToList();
        return AssetList;
    }

    public async Task<IList<AssetEventsEntity>?> GetLoansAssetsByUserId(string idUser)
    {
        var AssetList = dbSet.Where(a => a.User.IdUser == idUser).ToList();
        return AssetList;
    }
}
