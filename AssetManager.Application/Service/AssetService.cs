using AssetManager.Application.DTO.Asset;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;

namespace AssetManager.Application.Service
{
    public class AssetService: IAssetService
    {
        private IAssetRepository _assetRepository;
        private IMapper _mapper;
        private ICompanyRepository _companyRepository;

        public AssetService(IAssetRepository assetRepository, IMapper mapper, ICompanyRepository companyRepository)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public Task CreateAsset(AssetDTO asset)
        {
            try
            {
                AssetEntity assetEntity = _mapper.Map<AssetEntity>(asset);
                var company = _companyRepository.GetById(asset.IdCompany).Result;

                if (company == null)
                    throw new NullReferenceException($"A Companhia de id {asset.IdCompany} não existe!");

                return _assetRepository.Create(assetEntity);
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task UpdateAsset(UpdateAssetDTO asset)
        {
            try
            {
                AssetEntity? assetEntity = _mapper.Map<AssetEntity>(asset);
                return _assetRepository.Update(assetEntity);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
    
        public Task<IList<AssetDTO>> GetAssetsByCompany(int idCompany)
        {
            var assets = _assetRepository.GetAssetsByCompany(idCompany).Result;

            return Task.FromResult(_mapper.Map<IList<AssetDTO>>(assets));
        }

        public Task<AssetDTO?> GetByID(int idAsset)
        {
            var asset = _assetRepository.GetById(idAsset).Result;

           return Task.FromResult(_mapper.Map<AssetDTO?>(asset));
        }               


        public Task DeleteAsset(int idAsset, string? exclusionInfo)
        {
            try
            {
                return _assetRepository.Delete(idAsset, exclusionInfo);
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }

    }
}
