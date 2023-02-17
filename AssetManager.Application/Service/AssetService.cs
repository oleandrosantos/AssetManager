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

                var result = _assetRepository.Create(assetEntity);

                return Task.CompletedTask;
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
                var result = _assetRepository.Update(assetEntity);
                return Task.CompletedTask;
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

        public Task DeleteAsset(int idAsset, string exclusionInfo)
        {
            var result = _assetRepository.Delete(idAsset);

            if (result.IsCompletedSuccessfully)
                return Task.FromResult($"Asset removido com sucesso");

            return Task.FromResult("Erro, não foi possivel removido o Asset");
        }

    }
}
