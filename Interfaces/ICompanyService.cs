using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Interfaces
{
    public interface ICompanyService
    {
        public CompanyModel? ObterCompanyPorId(int idCompany);
        public Task<Result> CreateCompany(CreateCompanyViewModel company);
        public bool UpdateCompany(CompanyModel company);
        public Task<bool> DeleteCompany(int id);
        public List<CompanyModel> ListarCompany();
    }
}
