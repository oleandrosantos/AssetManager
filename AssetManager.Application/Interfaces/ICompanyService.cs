using AssetManager.Application.DTO.Company;

namespace AssetManager.Application.Interfaces;
public interface ICompanyService
{
    Task<string> CreateCompany(CreateCompanyDTO company);
    Task<string> UpdateCompany(CompanyDTO companyDTO);
    Task<CompanyDTO?> GetCompany(int id);
}
