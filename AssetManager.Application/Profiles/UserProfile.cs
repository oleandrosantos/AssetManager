using AssetManager.Application.DTO.User;
using AssetManager.Domain.Entities;
using AutoMapper;

namespace AssetManager.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        { 
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<UpdateUserDTO, UserEntity>().ReverseMap();
        }
    }
}
