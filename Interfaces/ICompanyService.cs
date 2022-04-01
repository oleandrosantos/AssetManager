using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Interfaces
{
    public interface ICompanyService
    {
        public CompanyModel? ObterCompanyPorId();
        public Result CreateCompany(CompanyModel company);
        public bool UpdateCompany(int id);
        public bool DeleteCompany(int id);
    }
}
