using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;

namespace AssetManager.Infra.Data.Repository;
public class AssetEventsRepository : RepositoryBase<AssetEventsEntity>, IAssetEventsRepository
{
	public AssetEventsRepository(DataContext dbContext) : base(dbContext) { }

    public Task LoanAsset(AssetEventsEntity assetEvent)
    {
        context.assetEvents.Add(assetEvent);
        context.SaveChanges();
        return Task.FromResult(assetEvent.IdAsset);
    }
}
