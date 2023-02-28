using AssetManager.Application.DTO.Asset;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Application.DTO.Company;
using AssetManager.Application.DTO.User;
using AssetManager.Domain.Entities;
using AutoMapper;

namespace AssetManager.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssetEntity, AssetDTO>();

            CreateMap<AssetDTO, AssetEntity>();
            CreateMap<UpdateAssetDTO, AssetEntity>();

            CreateMap<AssetEventsDTO, AssetEventsEntity>().ReverseMap();

            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<CreateUserDTO, UserEntity>().ReverseMap();
            CreateMap<UpdateUserDTO, UserEntity>().ReverseMap(); 
            
            CreateMap<CompanyDTO, CompanyEntity>().ReverseMap();
            CreateMap<CreateCompanyDTO, CompanyEntity>().ReverseMap();
        }
    }
}
