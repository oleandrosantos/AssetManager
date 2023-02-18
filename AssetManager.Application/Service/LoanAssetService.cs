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

        public Task<LoanAssetDTO?> GetLoanByID(string id)
        {
            try
            {
                var loanAssets = _loanAssetRepository.GetById(id).Result;
                return Task.FromResult(_mapper.Map<LoanAssetDTO?>(loanAssets));
            }
            catch (Exception e)
            {
                return Task.FromException<LoanAssetDTO?>(e);
            }
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

        public Task<IList<LoanAssetDTO>?> GetLoanAssetsByCompany(int idCompany)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<LoanAssetDTO>?>(_loanAssetRepository.GetByCompanyId(idCompany)));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<LoanAssetDTO>?>(e);
            }
        }
        public Task<IList<LoanAssetDTO>?> GetLoanAssetsByUser(string idUsuario)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<LoanAssetDTO>?>(_loanAssetRepository.GetByUserId(idUsuario)));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<LoanAssetDTO>?>(e);
            }
        }

        public Task<IList<LoanAssetDTO>?> GetLoanAssetsByAsset(int idAsset)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<LoanAssetDTO>?>(_loanAssetRepository.GetByAssetId(idAsset)));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<LoanAssetDTO>?>(e);
            }
        }

        public Task DevolutionAsset(TerminationLoanAssetModel terminate)
        {
            try
            {
                var loanAsset = GetLoanByID(terminate.LoanAssetId).Result;
                loanAsset.DevolutionDate = terminate.Date;
                loanAsset.Description = terminate.Description;
                _loanAssetRepository.Update(_mapper.Map<LoanAssetEntity>(loanAsset));
                return Task.CompletedTask;
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }
    }
}
