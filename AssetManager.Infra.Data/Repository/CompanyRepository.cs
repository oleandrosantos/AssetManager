using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.Repository
{
    public class CompanyRepository : RepositoryBase<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(DataContext dbContext) : base(dbContext) { }


    }
}
