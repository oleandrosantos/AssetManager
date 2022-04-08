using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.Repository;
using AssetManager.ViewModel;

namespace AssetManager.Service
{
    public class CompanyService : ICompanyService
    {
        private CompanyRepository _companyRepository;

        public CompanyService(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Result CreateCompany(CompanyModel company)
        {
            return _companyRepository.CreateCompany(company);
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

        public CompanyModel ObterCompanyPorId(int id)
        {
            return _companyRepository.ObterCompanyPorId(id);
        }

        public bool UpdateCompany(CompanyModel company)
        {
            return _companyRepository.UpdateCompany(company);
        }
        
    }
}
