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

        public List<LoanAssetModel> CompanyLoanAssetsList(int idCompany)
        {
            return _context.loanAsset
            .Where(l => l.Company.IdCompany == idCompany && l.DevolutionDate == null)
            .Include(l => l.Asset)
            .ToList();
        }

        public LoanAssetModel? GetByID(int id)
        {
            return _context.loanAsset.Find(id);
        }
    }
}
