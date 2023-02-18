using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Maoli;

namespace AssetManager.Infra.Data.Repository
{
    public class CompanyRepository : RepositoryBase<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(DataContext dbContext) : base(dbContext) { }

        public override Task Delete(int id)
        {
            var company = GetById(id).Result;

            if(company == null)
                throw new NullReferenceException(nameof(company));

            company.IsAtiva = false;
            company.ExclusionDate= DateTime.Now;
            context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
