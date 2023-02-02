using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
System.Data.Linq;

namespace AssetManager.Infra.Data.Repository;
public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DataContext Context;

    public RepositoryBase(DataContext dbContext)
    {
        Context = dbContext;
    }
    public virtual async Task<TEntity?> GetById(int id)
    {
        return await Context.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task Delete(int id)
    {
        TEntity entity = GetById(id).Result;
        if (entity != null)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

    public virtual async Task Create(TEntity entity)
    {
        GetTable().Ins
        Context.Add(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual ITable GetTable()
    {
        return Context.GetTable<TEntity>();
    }
}
