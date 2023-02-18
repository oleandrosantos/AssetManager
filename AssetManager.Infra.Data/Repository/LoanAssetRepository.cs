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
    public class LoanAssetRepository : RepositoryBase<LoanAssetEntity>, ILoanAssetRepository
    {
        public LoanAssetRepository(DataContext dbContext) : base(dbContext) { }

        public async Task<LoanAssetEntity?> GetById(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IList<LoanAssetEntity>?> GetByAssetId(int idAsset)
        {
            var AssetList = dbSet.Where(a => a.Asset.IdAsset == idAsset).ToList();
            return AssetList;
        }

        public async Task<IList<LoanAssetEntity>?> GetByCompanyId(int idCompany)
        {
            var AssetList = dbSet.Where(a => a.Company.IdCompany == idCompany).ToList();
            return AssetList;
        }

        public async Task<IList<LoanAssetEntity>?> GetByUserId(string idUser)
        {
            var AssetList = dbSet.Where(a => a.User.IdUser == idUser).ToList();
            return AssetList;
        }

        public Task CloseLoan(string id, string Description, DateTime Fi)
        {
            throw new NotImplementedException();
        }
    }
}
