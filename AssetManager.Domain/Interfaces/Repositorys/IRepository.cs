using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.Interfaces.Repositorys;
public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task Create(TEntity obj);
    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Delete(int id);
    Task Update(TEntity obj);
}
