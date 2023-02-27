using AssetManager.Application.DTO.Asset;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using AssetManager.Application.Profiles;
using AutoMapper;

namespace AssetManager.Application.Profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<AssetEntity, AssetDTO>()
                .ForMember(dest => dest.AssetEvents, opt => opt.MapFrom(src => src.AssetEvents));

            CreateMap<AssetDTO, AssetEntity>();
            CreateMap<UpdateAssetDTO, AssetEntity>();
        }
    }
}
