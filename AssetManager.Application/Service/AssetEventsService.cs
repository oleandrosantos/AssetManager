using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Service
{
    public class AssetEventsService:IAssetEventsService
    {
        private readonly IAssetEventsRepository _assetEventsRepository;
        private readonly IAssetService _assetRepository;
        private readonly IMapper _mapper;
        public AssetEventsService(IAssetEventsRepository assetEventsRepository, IMapper mapper, IAssetService assetRepository)
        {
            _assetEventsRepository = assetEventsRepository;
            _mapper = mapper;
            _assetRepository = assetRepository;
        }
        public Task LoanAsset(LoanAssetDTO loanAsset)
        {
            try
            {
                var asset = _assetRepository.GetByID(loanAsset.IdAsset).Result;
                if (asset.IsLoanable())
                    return Task.FromException(new Exception("O asset já esta locado!"));
                
                _assetEventsRepository.LoanAsset(_mapper.Map<AssetEventsEntity>(loanAsset));
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task<IList<AssetEventsDTO>?> GetLoanAssetsByCompany(int idCompany)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<AssetEventsDTO>?>(_assetEventsRepository.GetLoansAssetsByCompanyId(idCompany).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<AssetEventsDTO>?>(e);
            }
        }
        public Task<IList<AssetEventsDTO>?> GetLoanAssetsByUser(string idUsuario)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<AssetEventsDTO>?>(_assetEventsRepository.GetLoansAssetsByUserId(idUsuario).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<AssetEventsDTO>?>(e);
            }
        }

        public Task<IList<AssetEventsDTO>?> GetLoanAssetsByAsset(int idAsset)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<AssetEventsDTO>?>(_assetEventsRepository.GetLoansAssetsByAssetId(idAsset).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<AssetEventsDTO>?>(e);
            }
        }

        public Task<AssetEventsDTO?> GetAssetEventsById(int idAsset)
        {
            try
            {
                return Task.FromResult(_mapper.Map<AssetEventsDTO?>(_assetEventsRepository.GetByID(idAsset).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<AssetEventsDTO?>(e);
            }
        }

        public Task TerminateLoanContract(TerminationLoanAssetModel terminate)
        {
            try
            {
                var loanAsset = GetLoanAssetsByAsset(terminate.IdAsset).Result.LastOrDefault();
                loanAsset.IdEvent = 0;
                loanAsset.EventDate = terminate.Date.GetValueOrDefault();
                loanAsset.Description = terminate.Description;
                loanAsset.IdUserRegister = terminate.IdUserRegister;
                _assetEventsRepository.Create(_mapper.Map<AssetEventsEntity>(loanAsset));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

    }
}
