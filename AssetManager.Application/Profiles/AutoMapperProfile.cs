using AssetManager.Application.DTO.Ativo;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Application.DTO.Companhia;
using AssetManager.Application.DTO.Usuario;
using AssetManager.Domain.Entities;
using AutoMapper;

namespace AssetManager.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AtivoEntity, AtivoDTO>();

            CreateMap<AtivoDTO, AtivoEntity>();
            CreateMap<AtualizarAtivoDTO, AtivoEntity>();

            CreateMap<EventosAtivoDTO, EventosAtivoEntity>().ReverseMap();

            CreateMap<UsuarioDTO, UsuarioEntity>().ReverseMap();
            CreateMap<CriarUsuarioDTO, UsuarioEntity>().ReverseMap();
            CreateMap<AtualizarUsuarioDTO, UsuarioEntity>().ReverseMap(); 
            
            CreateMap<CompanhiaDTO, CompanhiaEntity>().ReverseMap();
            CreateMap<CriarCompanhiaDTO, CompanhiaEntity>().ReverseMap();
        }
    }
}
