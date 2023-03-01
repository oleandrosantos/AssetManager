using AssetManager.Application.DTO.Ativo;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;

namespace AssetManager.Application.Service
{
    public class AtivosService: IAtivosService
    {
        private IAtivosRepository _ativosRepository;
        private IMapper _mapper;
        private ICompanhiaRepository _companhiaRepository;

        public AtivosService(IAtivosRepository ativosRepository, IMapper mapper, ICompanhiaRepository companhiaRepository)
        {
            _ativosRepository = ativosRepository;
            _mapper = mapper;
            _companhiaRepository = companhiaRepository;
        }

        public Task CriarAtivo(AtivoDTO asset)
        {
            try
            {
                AtivoEntity assetEntity = _mapper.Map<AtivoEntity>(asset);
                var company = _companhiaRepository.ObterCompanhiaPorId(asset.IdCompanhia).Result;

                if (company == null)
                    throw new NullReferenceException($"A Companhia de id {asset.IdCompanhia} não existe!");

                return _ativosRepository.Cadastrar(assetEntity);
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task AtualizarAtivo(AtualizarAtivoDTO asset)
        {
            try
            {
                AtivoEntity? assetEntity = _mapper.Map<AtivoEntity>(asset);
                return _ativosRepository.Atualizar(assetEntity);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
    
        public Task<IList<AtivoDTO>> ObterTodosAtivosDaCompanhia(int IdCompanhia)
        {
            var assets = _ativosRepository.ObterAtivosDaCompanhia(IdCompanhia).Result;

            return Task.FromResult(_mapper.Map<IList<AtivoDTO>>(assets));
        }

        public Task<AtivoDTO?> ObterAtivoPorID(int idAtivo)
        {
            try
            {
               var asset = _ativosRepository.ObterAtivoPorId(idAtivo).Result;

               return Task.FromResult(_mapper.Map<AtivoDTO?>(asset));
            }
            catch(Exception e)
            {
                return Task.FromException<AtivoDTO?>(e);
            }
        }               


        public Task DeletarAtivo(int idAtivo, string? exclusionInfo)
        {
            try
            {
                return _ativosRepository.Delete(idAtivo, exclusionInfo);
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }

    }
}
