using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Interfaces
{
    public interface ICompanyService
    {
        public CompanyModel? ObterCompanyPorId(int idCompany);
        public Result CreateCompany(CompanyModel company);
        public bool UpdateCompany(CompanyModel company);
        public Task<bool> DeleteCompany(int id);
    }
}
