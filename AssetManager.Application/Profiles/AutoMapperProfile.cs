using AssetManager.Application.DTO.Ativo;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Application.DTO.Companhia;
using AssetManager.Application.DTO.Usuario;
using AssetManager.Domain.Entities;
using AutoMapper;
using AssetManager.Application.Enums;

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
            CreateMap<CriarUsuarioDTO, UsuarioEntity>()
                .ForMember(dst => dst.Role, src => src.MapFrom(src => RolesUsuarios.Funcionario.Description()));

            CreateMap<AtualizarUsuarioDTO, UsuarioEntity>().ReverseMap(); 
            
            CreateMap<CompanhiaDTO, CompanhiaEntity>().ReverseMap();
            CreateMap<CriarCompanhiaDTO, CompanhiaEntity>().ReverseMap();
        }
    }
}
