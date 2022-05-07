using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.Repository;
using AssetManager.ViewModel;
using AutoMapper;

namespace AssetManager.Service
{
    public class LocationAssetService : ILocationAssetService
    {
        private LocationAssetRepository _locationAssetRepository;
        private IMapper _mapper;
        private UserRepository _userRepository;
        private CompanyRepository _companyRepository;

        public LocationAssetService(LocationAssetRepository locationAssetRepository, IMapper mapper, UserRepository userRepository, CompanyRepository companyRepository)
        {
            _locationAssetRepository = locationAssetRepository;
            _mapper = mapper;
        }

        public Result CreateLocationAsset(CreateLocationAsset locationAsset)
        {
            LocationAssetModel locationAssetModel = _mapper.Map<CreateLocationAsset, LocationAssetModel>(locationAsset);
            locationAssetModel.asset.idAsset = locationAsset.idAsset;
            locationAssetModel.usuario.idUsuario = locationAsset.idUsuario;

            return _locationAssetRepository.CreateLocationAsset(locationAssetModel);
        }

        public Result DeleteLocationAsset(int id)
        {
            throw new NotImplementedException();
        }

        public List<LocationAssetModel> CompanyLocationAssetsList(int idCompany) => _locationAssetRepository.CompanyLocationAssetsList(idCompany);

        public Result UpdateLocationAsset(LocationAssetModel locationAssetModel)
        {
            throw new NotImplementedException();
        }

        public List<LocationAssetModel> UserAssetLocationList(string idUser) => _locationAssetRepository.UserAssetLocationList(idUser);

    }
}
