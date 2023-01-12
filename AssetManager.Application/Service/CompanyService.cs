using AssetManager.Application.Interfaces;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.DTO;
using AssetManager.Infra.Data.DTO.Company;
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
        public Task<ResultOperation> CreateCompany(CreateCompanyDTO company)
        {
            CompanyEntity? companyEntity = _mapper.Map<CompanyEntity>(company);

            if (companyEntity == null)
                return Task.FromResult(new ResultOperation("Houve um erro no cadasro da Companhia"));
   
            return Task.FromResult(new ResultOperation("Compnhia Cadastrada com sucesso", true));
            
        }

        Task<CompanyDTO> ICompanyService.GetCompany(int id)
        {
            throw new NotImplementedException();
        }

        Task<ResultOperation> ICompanyService.UpdateCompany(CompanyDTO companyDTO)
        {
            throw new NotImplementedException();
        }
    }
}
