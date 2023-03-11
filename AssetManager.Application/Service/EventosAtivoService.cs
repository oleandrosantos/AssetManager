using AssetManager.Application.DTO.Ativo;
using AssetManager.Application.DTO.AssetEvents;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Service
{
    public class EventosAtivoService:IEventosAtivosService
    {
        private readonly IEventosAtivosRepository _eventosAtivosRepository;
        private readonly IAtivosRepository _ativosRepository;
        private readonly IMapper _mapper;
        public EventosAtivoService(IEventosAtivosRepository eventosAtivosRepository, IMapper mapper, IAtivosRepository ativosRepository)
        {
            _eventosAtivosRepository = eventosAtivosRepository;
            _mapper = mapper;
            _ativosRepository = ativosRepository;
        }
        public Task EmprestarAtivo(EmpretimoAtivoDTO loanAsset)
        {
            try
            {
                var ativo = _mapper.Map<AtivoDTO>(_ativosRepository.ObterAtivoPorId(loanAsset.IdAtivo).Result);
                if (!ativo.ELocavel())
                    return Task.FromException(new Exception("O ativo já esta locado!"));
                
                _eventosAtivosRepository.EmprestarAtivo(_mapper.Map<EventosAtivoEntity>(loanAsset));
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task<IList<EventosAtivoDTO>?> ObterTodosOsEventosDosAtivosDaCompanhia(int IdCompanhia)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<EventosAtivoDTO>?>(_eventosAtivosRepository.ObterTodosOsEventosDosAtivosDaCompanhia(IdCompanhia).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<EventosAtivoDTO>?>(e);
            }
        }
        public Task<IList<EventosAtivoDTO>?> ObterTodosOsEventosDosAtivosDoUsuario(string idUsuario)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<EventosAtivoDTO>?>(_eventosAtivosRepository.ObterTodosOsEventosDosAtivosDoUsuario(idUsuario).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<EventosAtivoDTO>?>(e);
            }
        }

        public Task<IList<EventosAtivoDTO>?> ObterTodosOsEventosDoAtivo(int idAtivo)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IList<EventosAtivoDTO>?>(_eventosAtivosRepository.ObterTodosOsEventosDoAtivo(idAtivo).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<IList<EventosAtivoDTO>?>(e);
            }
        }

        public Task<EventosAtivoDTO?> ObterEventosPorId(int idAtivo)
        {
            try
            {
                return Task.FromResult(_mapper.Map<EventosAtivoDTO?>(_eventosAtivosRepository.ObterPorId(idAtivo).Result));
            }
            catch (Exception e)
            {
                return Task.FromException<EventosAtivoDTO?>(e);
            }
        }

        public Task EncerrarEmprestimoDoAtivo(FimEmprestimoAtivoDTO terminate)
        {
            try
            {
                var loanAsset = ObterTodosOsEventosDoAtivo(terminate.IdAtivo).Result.LastOrDefault();
                loanAsset.IdEventosAtivo = 0;
                loanAsset.DataEvento = terminate.DataEvento.GetValueOrDefault();
                loanAsset.Descricao = terminate.Descricao;
                loanAsset.IdUsuarioRegistro = terminate.IdUsuarioRegistro;
                _eventosAtivosRepository.Cadastrar(_mapper.Map<EventosAtivoEntity>(loanAsset));
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

    }
}
