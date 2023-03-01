using AssetManager.Application.DTO.Companhia;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Validations;
using AutoMapper;

namespace AssetManager.Application.Service
{
    public class CompanhiaService : ICompanhiaService
    {
        private readonly ICompanhiaRepository _companhiaRepository;
        private readonly IMapper _mapper;

        public CompanhiaService(ICompanhiaRepository companhiaRepository, IMapper mapper)
        {
            _companhiaRepository = companhiaRepository;
            _mapper = mapper; 
        }
        public Task CriarCompanhia(CriarCompanhiaDTO company)
        {
            try
            {
                CompanhiaEntity? companyEntity = _mapper.Map<CompanhiaEntity>(company);

                if (companyEntity == null)
                    throw new NullReferenceException(nameof(companyEntity));

                return _companhiaRepository.Cadastrar(companyEntity);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task<CompanhiaDTO?> ObterCompanhiaPorId(int id)
        {
            try
            {
                CompanhiaEntity? company = _companhiaRepository.ObterCompanhiaPorId(id).Result;
                if (company == null)
                    throw new ObjetoVazioException("Companhia não localizada!");

                return Task.FromResult(_mapper.Map<CompanhiaDTO?>(company));
            }
            catch(Exception)
            {
                throw;
            }

        }

        public Task AtualizarCompanhia(CompanhiaDTO companhiaDTO)
        {
            try
            {
                _companhiaRepository.Atualizar(_mapper.Map<CompanhiaEntity>(companhiaDTO));
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task DeletarCompanhia(int IdCompanhia)
        {
            try
            {
                _companhiaRepository.Delete(IdCompanhia);
                return Task.CompletedTask;
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task<List<CompanhiaDTO>> ObterTodasAsCompanhias()
        {
            try
            {
                var companyList = _companhiaRepository.ObterTodasAsCompanhias().Result;
                var company = _mapper.Map<List<CompanhiaDTO>>(companyList);
                
                return Task.FromResult(company);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
