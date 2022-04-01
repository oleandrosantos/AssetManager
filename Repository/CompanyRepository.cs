using AssetManager.Data;
using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Repository;

public class CompanyRepository :ICompanyService
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

    public async Result CreateCompany(CompanyModel company)
    {
        if (CompanyExists(company))
            return new Result(false, $"Esta companhia ja esta cadastrada em nosso sistema!");

        _context.company.Add(company);
        await _context.SaveChangesAsync();
        return new Result(true, $"{company.companyName} cadastrada com sucesso");
    }

    public bool DeleteCompany(int id)
    {
        throw new NotImplementedException();
    }

    public CompanyModel? ObterCompanyPorId()
    {
        throw new NotImplementedException();
    }

    public bool UpdateCompany(int id)
    {
        throw new NotImplementedException();
    }
    
    private bool CompanyExists(CompanyModel company)
    {
        var DataCompany = _context.company.Where(c => c.cnpj == company.cnpj && c.companyName.ToLower().Trim() == company.companyName.ToLower().Trim())
            .FirstOrDefault();

        if (DataCompany != null)
            return false;

        return true;
    }
}