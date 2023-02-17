using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Profiles
{
    public class LoanAssetProfile : Profile
    {
        public LoanAssetProfile()
        {
            CreateMap<LoanAssetDTO, LoanAssetEntity>();
            CreateMap<TerminationLoanAssetModel, LoanAssetEntity>();
        }
    }
}
