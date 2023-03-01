using AssetManager.Domain.Entities;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Infra.Data.Context;
using Maoli;

namespace AssetManager.Infra.Data.Repository
{
    public class CompanhiaRepository : RepositoryBase<CompanhiaEntity>, ICompanhiaRepository
    {
        public CompanhiaRepository(DataContext dbContext) : base(dbContext) { }

        public Task Delete(int id)
        {
            var company = ObterCompanhiaPorId(id).Result;

            if(company == null)
                throw new NullReferenceException(nameof(company));

            company.Ativa = false;
            company.ExclusionDate= DateTime.Now;
            context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task<CompanhiaEntity?> ObterCompanhiaPorId(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IList<CompanhiaEntity>> ObterTodasAsCompanhias()
        {
            return dbSet.Where(a => a.Ativa).ToList();
        }
    }
}
