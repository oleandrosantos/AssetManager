using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IAssetEventsRepository : IRepositoryBase<AssetEventsEntity>
{
    Task LoanAsset(AssetEventsEntity assetEvent);
}