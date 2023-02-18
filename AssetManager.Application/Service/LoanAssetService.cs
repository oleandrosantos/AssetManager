using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;

namespace AssetManager.Application.Service
{
    public class LoanAssetService : ILoanAssetService
    {
        private readonly ILoanAssetRepository _loanAssetRepository;
        private readonly IMapper _mapper;

        public LoanAssetService(ILoanAssetRepository loanAssetRepository, IMapper mapper)
        {
            _loanAssetRepository = loanAssetRepository;
            _mapper = mapper;
        }

        public Task DevolutionAsset(TerminationLoanAssetModel terminate)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoanAssetDTO>> GetActiveLoansByAssets(int idAsset)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoanAssetDTO>> GetActiveLoansByCompany(int idCompany)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoanAssetDTO>> GetLoanAssetsByAssets(int idAsset)
        {
            _loanAssetRepository.GetById(ObjectDisposedException )
            throw new NotImplementedException();
        }

        public Task<List<LoanAssetDTO>> GetLoanAssetsByCompany(int idCompany)
        {
            throw new NotImplementedException();
        }

        public Task<LoanAssetDTO> GetLoanByID(string idLoanAsset)
        {
            throw new NotImplementedException();
        }

        public Task LoanAsset(LoanAssetDTO loanAsset)
        {
            try
            {
                LoanAssetEntity entity = _mapper.Map<LoanAssetEntity>(loanAsset);
                return _loanAssetRepository.Create(entity);
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }

        }

        public Task<List<LoanAssetDTO>> UserAssetLoanList(string idUser)
        {
            throw new NotImplementedException();
        }

        Task ILoanAssetService.LoanAsset(LoanAssetDTO loanAsset)
        {
            throw new NotImplementedException();
        }
    }
}
