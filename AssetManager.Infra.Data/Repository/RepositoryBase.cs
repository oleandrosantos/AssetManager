using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AssetManager.Infra.Data.Repository;
public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DataContext Context;
    protected readonly DbSet<TEntity> dbSet;

    public RepositoryBase(DataContext dbContext)
    {
        Context = dbContext;
        dbSet = dbContext.Set<TEntity>();
    }
    public virtual async Task<TEntity?> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task Delete(int id)
    {
        TEntity entity = GetById(id).Result;
        if (entity != null)
        {
            dbSet.Remove(entity);
            Context.SaveChanges();
        }
    }

    public virtual async Task Create(TEntity entity)
    {
        dbSet.Add(entity);
        Context.SaveChanges();
    }

    public virtual async Task Update(TEntity entity)
    {
        dbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
    }
}
