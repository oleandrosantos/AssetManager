using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.Interfaces;
public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task Insert(TEntity obj);
    Task<TEntity?> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Delete(TEntity obj);
    Task Update(TEntity obj);
}
