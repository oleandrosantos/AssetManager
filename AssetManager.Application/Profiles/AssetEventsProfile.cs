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
    public class AssetEventsProfile : Profile
    {
        public AssetEventsProfile()
        {
            CreateMap<LoanAssetDTO, AssetEventsEntity>()
                .ForMember(l => l.IdAsset, src => src.MapFrom(a => a.IdAsset))
                .ForMember(l => l.IdUserRegister, src => src.MapFrom(a => a.IdUser))
        }
    }
}
