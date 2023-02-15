using AssetManager.Application.DTO.Company;

namespace AssetManager.Application.Interfaces;
public interface ICompanyService
{
    Task Create(CreateCompanyDTO company);
    Task Update(CompanyDTO companyDTO);
    Task<CompanyDTO?> GetByID(int id);
    Task Delete(int idCompany);
    Task<List<CompanyDTO>> GetAll();
}
