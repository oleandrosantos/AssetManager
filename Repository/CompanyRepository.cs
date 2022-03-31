using AssetManager.Data;
using AssetManager.Model;

namespace AssetManager.Repository;

public class CompanyRepository
{
    private DataContext _context;

    public CompanyRepository(DataContext context)
    {
        _context = context;
    }

    public CompanyModel? ObterCompanyPorId(int idCompany)
    {
       return _context.company
           .FirstOrDefault(c => c.idCompany == idCompany);
    }
}