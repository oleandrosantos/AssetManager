﻿using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AssetManager.Infra.Data.Repository;
public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DataContext context;
    protected readonly DbSet<TEntity> dbSet;

    public RepositoryBase(DataContext dbContext)
    {
        context = dbContext;
        dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task Cadastrar(TEntity entity)
    {
        dbSet.Add(entity);
        context.SaveChanges();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        dbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }
}
