using AssetManager.Application.DTO.Company;
using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AutoMapper;

namespace AssetManager.Application.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper; 
        }
        public Task CreateCompany(CreateCompanyDTO company)
        {
            try
            {
                CompanyEntity? companyEntity = _mapper.Map<CompanyEntity>(company);

                if (companyEntity == null)
                    throw new NullReferenceException(nameof(companyEntity));

                var result = _companyRepository.Create(companyEntity);
                return Task.CompletedTask;

            }
            catch(Exception ex)
            {
                return Task.FromException(ex);
            }

        }

        public Task<CompanyDTO?> GetCompany(int id)
        {
            try
            {
                CompanyEntity? company = _companyRepository.GetById(id).Result;

                return Task.FromResult(_mapper.Map<CompanyDTO?>(company));
            }
            catch(Exception ex)
            {
                return Task.FromException<CompanyDTO?>(ex);
            }

        }

        public Task UpdateCompany(CompanyDTO companyDTO)
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
    }
}
