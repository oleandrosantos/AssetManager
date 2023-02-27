using AssetManager.Application.DTO.Company;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Validations;
using AutoMapper;

namespace AssetManager.Application.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper; 
        }
        public Task Create(CreateCompanyDTO company)
        {
            try
            {
                CompanyEntity? companyEntity = _mapper.Map<CompanyEntity>(company);

                if (companyEntity == null)
                    throw new NullReferenceException(nameof(companyEntity));

                return _companyRepository.Create(companyEntity);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task<CompanyDTO?> GetByID(int id)
        {
            try
            {
                CompanyEntity? company = _companyRepository.GetById(id).Result;
                if (company == null)
                    throw new EmptyReturnException("Companhia não localizada!");

                return Task.FromResult(_mapper.Map<CompanyDTO?>(company));
            }
            catch(Exception)
            {
                throw;
            }

        }

        public Task Update(CompanyDTO companyDTO)
        {
            try
            {
                _companyRepository.Update(_mapper.Map<CompanyEntity>(companyDTO));
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task Delete(int idCompany)
        {
            try
            {
                _companyRepository.Delete(idCompany);
                return Task.CompletedTask;
            }
            catch(Exception e)
            {
                return Task.FromException(e);
            }
        }

        public Task<List<CompanyDTO>> GetAll()
        {
            try
            {
                var companyList = _companyRepository.GetAll().Result;
                var company = _mapper.Map<List<CompanyDTO>>(companyList);
                
                return Task.FromResult(company);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
