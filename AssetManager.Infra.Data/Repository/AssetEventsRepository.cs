using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.Repository;
public class AssetEventsRepository : RepositoryBase<AssetEventsEntity>, IAssetEventsRepository
{
	public AssetEventsRepository(DataContext dbContext) : base(dbContext) { }
}
