using AssetManager.Domain.Entities;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface ICompanyRepository : IRepositoryBase<CompanyEntity>
{
    Task<CompanyEntity?> GetById(int id);
    Task Delete(int id);
}