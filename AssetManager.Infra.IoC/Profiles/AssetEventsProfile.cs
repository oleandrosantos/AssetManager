using AssetManager.Application.DTO.LoanAsset;
using AssetManager.Domain.Entities;
using AutoMapper;

namespace AssetManager.Application.Profiles
{
    public class AssetEventsProfile : Profile
    {
        public AssetEventsProfile()
        {
            CreateMap<LoanAssetDTO, AssetEventsEntity>()
                .ReverseMap();

            CreateMap<AssetEventsDTO, AssetEventsEntity>()
                .ReverseMap();
        }
    }
}
