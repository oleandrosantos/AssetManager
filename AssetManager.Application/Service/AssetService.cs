using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.DTO;
using AssetManager.Infra.Data.DTO.Asset;
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

        public Task<ResultOperation> CreateAsset(AssetDTO asset)
        {
            AssetEntity? assetEntity = _mapper.Map<AssetDTO, AssetEntity>(asset);
            var company = _companyRepository.GetById(asset.IdCompany).Result;
            if (company != null)
                assetEntity.Company = company;

            var result = _assetRepository.Create(assetEntity);

            if (result.IsCompletedSuccessfully)
                return Task.FromResult(new ResultOperation($"{assetEntity.AssetName} cadastrado com sucesso", true));

            return Task.FromResult(new ResultOperation("Erro, não foi possivel criar o Asset"));
        }

        public Task<ResultOperation> UpdateAsset(UpdateAssetDTO asset)
        {
            AssetEntity? assetEntity = _mapper.Map<UpdateAssetDTO, AssetEntity>(asset);
            var result = _assetRepository.Update(assetEntity);

            if (result.IsCompletedSuccessfully)
                return Task.FromResult(new ResultOperation($"{asset.AssetName} Atualizar com sucesso", true));

            return Task.FromResult(new ResultOperation("Erro, não foi possivel Atualizar o Asset"));
        }
    
        public List<AssetDTO> GetAssetsByCompany(int idCompany)
        {
            var assets = _assetRepository.GetAssetsByCompany(idCompany).Result;
            return _mapper.Map<List<AssetDTO>>(assets);
        }

        public AssetDTO? GetByID(int idAsset) => _mapper.Map<AssetDTO>(_assetRepository.GetById(idAsset).Result);

        public Task<ResultOperation> DeleteAsset(int idAsset, string exclusionInfo)
        {
            var result = _assetRepository.Delete(idAsset);

            if (result.IsCompletedSuccessfully)
                return Task.FromResult(new ResultOperation($"Asset removido com sucesso", true));

            return Task.FromResult(new ResultOperation("Erro, não foi possivel removido o Asset"));
        }

    }
}
