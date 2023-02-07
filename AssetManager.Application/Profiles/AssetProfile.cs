using AssetManager.Application.DTO.Asset;
using AssetManager.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<AssetDTO, AssetEntity>().ReverseMap();
            CreateMap<UpdateAssetDTO, AssetEntity>().ReverseMap();
        }
    }
}
