using AssetManager.Data;
using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

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

    public Result CreateCompany(CompanyModel company)
    {
        if (CompanyExists(company))
            return new Result(false, $"Esta companhia ja esta cadastrada em nosso sistema!");

        _context.company.Add(company);
        _context.SaveChangesAsync();
        return new Result(true, $"{company.companyName} cadastrada com sucesso");
    }

    public async Task<bool> DeleteCompany(int id)
    {
        var companyModel = await _context.company.FindAsync(id);
        if (companyModel == null)
        {
            return false;
        }

        _context.company.Remove(companyModel);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public bool UpdateCompany(CompanyModel company)
    {
        throw new NotImplementedException();
    }
    
    private bool CompanyExists(CompanyModel company)
    {
        CompanyModel DataCompany = _context.company
            .FirstOrDefault(c => c.cnpj == company.cnpj && c.companyName.ToLower().Trim() == company.companyName.ToLower().Trim());

        if (DataCompany != null)
            return false;

        return true;
    }
}