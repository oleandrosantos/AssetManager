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
            _context.loanAsset.Add(loanAsset);
            return new Result(true, "Criado com sucesso");
        }
        public List<LoanAssetModel> UserAssetLoanList(string idUser, bool isLocatad)
        {
            var loanAssetList = _context.loanAsset
                .Where(l => l.usuario.idUsuario == idUser)
                .Include(l => l.asset);

            if (isLocatad)
                loanAssetList.Where(l => l.devolutionDate == null);

            return loanAssetList.ToList();
        }

        public List<LoanAssetModel> CompanyLoanAssetsList(int idCompany)
        {
            return _context.loanAsset
            .Where(l => l.company.idCompany == idCompany && l.devolutionDate == null)
            .Include(l => l.asset)
            .ToList();
        }
    }
}
