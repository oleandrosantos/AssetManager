using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.EntityFrameworkCore;

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
        public List<LocationAssetModel> UserAssetLocationList(string idUser)
        {
            return _context.locationAsset
                .Where(l => l.usuario.idUsuario == idUser)
                .Include(l => l.asset)
                .ToList();
        }

        public List<LocationAssetModel> CompanyLocationAssetsList(int idCompany)
        {
            return _context.locationAsset
            .Where(l => l.company.idCompany == idCompany && l.devolutionDate == null)
            .Include(l => l.asset)
            .ToList();
        }
    }
}
