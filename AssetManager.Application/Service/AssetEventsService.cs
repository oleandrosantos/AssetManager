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
        private readonly IMapper _mapper;
        public AssetEventsService(IAssetEventsRepository assetEventsRepository, IMapper mapper)
        {
            _assetEventsRepository = assetEventsRepository;
            _mapper = mapper;
        }
        public Task LoanAsset(LoanAssetDTO loanAsset)
        {
            try
            {
                _assetEventsRepository.LoanAsset(_mapper.Map<AssetEventsEntity>(loanAsset));
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}
