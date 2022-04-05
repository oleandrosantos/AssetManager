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

        public CompanyModel? ObterCompanyPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCompany(CompanyModel company)
        {
            throw new NotImplementedException();
        }
    }
}
