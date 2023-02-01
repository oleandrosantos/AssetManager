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
        public Task<string> CreateCompany(CreateCompanyDTO company)
        {
            CompanyEntity? companyEntity = _mapper.Map<CompanyEntity>(company);

            if (companyEntity == null)
                return Task.FromResult("Houve um erro no cadasro da Companhia");

            var result = _companyRepository.Create(companyEntity);
            if (result.IsCompletedSuccessfully)
                return Task.FromResult("Houve um erro no cadasro da Companhia");

            return Task.FromResult("Companhia Cadastrada com sucesso");
            
        }

        public Task<CompanyDTO?> GetCompany(int id)
        {
            CompanyEntity? company = _companyRepository.GetById(id).Result;

            return Task.FromResult(_mapper.Map<CompanyDTO?>(company));
        }

        public Task<string> UpdateCompany(CompanyDTO companyDTO)
        {
            throw new NotImplementedException();
        }
    }
}
