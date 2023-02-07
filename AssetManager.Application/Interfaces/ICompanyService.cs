using AssetManager.Application.DTO.Company;

namespace AssetManager.Application.Interfaces;
public interface ICompanyService
{
    Task CreateCompany(CreateCompanyDTO company);
    Task UpdateCompany(CompanyDTO companyDTO);
    Task<CompanyDTO?> GetCompany(int id);
}
