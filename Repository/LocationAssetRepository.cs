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
        public List<LocationAssetModel> UserAssetLocationList(string idUser) => _context.locationAsset.Where(l => l.usuario.idUsuario == idUser).ToList();
        public List<LocationAssetModel> CompanyLocationAssetsList(int idCompany) => _context.locationAsset.Where(l => l.company.idCompany == idCompany && l.devolutionDate == null).ToList();
    }
}
