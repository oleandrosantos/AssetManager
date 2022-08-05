using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repository
{
    public class LoanAssetRepository
    {
        private DataContext _context;
        public LoanAssetRepository(DataContext context)
        {
            _context = context;
        }

        public Result CreateLoanAsset(LoanAssetModel loanAsset)
        {
            try
            {
                loanAsset.IdLoanAsset = Guid.NewGuid().ToString().Substring(0, 31);
                _context.loanAsset.Add(loanAsset);
                _context.SaveChanges();
                return new Result(true, $"Criado com sucesso Nr. CTO: {loanAsset.IdLoanAsset}");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public List<LoanAssetModel> UserAssetLoanList(string idUser, bool isLocatad)
        {
            var loanAssetList = _context.loanAsset
                .Where(l => l.Usuario.IdUsuario == idUser)
                .Include(l => l.Asset);

            if (isLocatad)
                loanAssetList.Where(l => l.DevolutionDate == null);

            return loanAssetList.ToList();
        }

        public async Task<IEnumerable<LoanAssetModel>> CompanyLoanAssetsList(int idCompany)
        {
            return await _context.loanAsset
            .Where(l => l.Company.IdCompany == idCompany && l.DevolutionDate == null)
            .Include(l => l.Asset)
            .ToListAsync();
        }

        public async Task<IEnumerable<LoanAssetModel>> LoanAssetList(LoanAssetFilter filter)
        {
            var loanAsset = _context.loanAsset.Include(l => l.Asset);;
            if (filter.IdUsuario != null)
                loanAsset.Where(l => l.IdUsuario == filter.IdUsuario);
            if(filter.IsAtive != null)
                if (filter.IsAtive == true)
                    loanAsset.Where(l => l.DevolutionDate == null);
                else
                    loanAsset.Where(l => l.DevolutionDate.HasValue);
            if(filter.IdCompany != null)
                loanAsset.Where(l => l.IdCompany == filter.IdCompany);

            return await loanAsset.ToListAsync();
        }

        public LoanAssetModel? GetByID(string id)
        {
            return _context.loanAsset.Find(id);
        }

        public bool UpdateLocationAsset(LoanAssetModel loan)
        {
            try
            {
                _context.loanAsset.Update(loan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
