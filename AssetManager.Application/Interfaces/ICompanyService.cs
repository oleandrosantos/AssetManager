using AssetManager.Infra.Data.DTO;
using AssetManager.Infra.Data.DTO.Company;

namespace AssetManager.Application.Interfaces;
public interface ICompanyService
{
    Task<ResultOperation> CreateCompany(CreateCompanyDTO company);
    Task<ResultOperation> UpdateCompany(CompanyDTO companyDTO);
    Task<CompanyDTO> GetCompany(int id);
}
