using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.Repository;
using AssetManager.ViewModel;
using AutoMapper;

namespace AssetManager.Service
{
    public class CompanyService : ICompanyService
    {
        private CompanyRepository _companyRepository;
        private IMapper _mapper;

        public CompanyService(CompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Result> CreateCompany(CreateCompanyViewModel company)
        {
            return _companyRepository.CreateCompany(_mapper.Map<CreateCompanyViewModel, CompanyModel>(company));
        }

        public async Task<bool>  DeleteCompany(int id)
        {
            bool result = await _companyRepository.DeleteCompany(id);
            return result;
        }

        public List<CompanyModel> ListarCompany()
        {
            return _companyRepository.ListarCompany();
        }

        public CompanyModel? ObterCompanyPorId(int id)
        {
            return _companyRepository.ObterCompanyPorId(id);
        }

        public bool UpdateCompany(CompanyModel company)
        {
            return _companyRepository.UpdateCompany(company);
        }
        
    }
}
