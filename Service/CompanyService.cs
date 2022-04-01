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

        public bool DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }

        public CompanyModel? ObterCompanyPorId()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCompany(int id)
        {
            throw new NotImplementedException();
        }
    }
}
