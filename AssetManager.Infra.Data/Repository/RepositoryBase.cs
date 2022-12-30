using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Infra.Data.Repository;
public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DataContext context;

    public RepositoryBase(DataContext dbContext)
    {
        context = dbContext;
    }
    public virtual async Task<TEntity?> GetById(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task Delete(int id)
    {
        TEntity entity = GetById(id).Result;
        if (entity != null)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public virtual async Task Create(TEntity entity)
    {
        context.Add(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }
}
