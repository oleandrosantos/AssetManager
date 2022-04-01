using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Repository
{
    public class LocationAssetRepository
    {
        private DataContext _context;
        public LocationAssetRepository(DataContext context)
        {
            _context = context;
        }

        public Result CreateLocationAsset(LocationAssetModel locationAsset)
        {
            _context.locationAsset.Add(locationAsset);
            return new Result(true, "Criado com sucesso");
        }
    }
}
