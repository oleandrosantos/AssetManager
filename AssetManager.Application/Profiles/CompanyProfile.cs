using AssetManager.Application.DTO.Company;
using AssetManager.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyDTO, CompanyEntity>().ReverseMap();
            CreateMap<CreateCompanyDTO, CompanyEntity>().ReverseMap();

        }
    }
}
