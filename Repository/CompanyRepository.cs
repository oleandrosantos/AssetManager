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

    public CompanyModel ObterCompanyPorId(int idCompany)
    {
        return _context.company.Find(idCompany);
    }

    public Result CreateCompany(CompanyModel company)
    {
        if (CompanyExists(company))
            return new Result(false, $"Esta companhia ja esta cadastrada em nosso sistema!");

        _context.company.Add(company);
        _context.SaveChangesAsync();
        return new Result(true, $"{company.companyName} cadastrada com sucesso");
    }

    public bool UpdateCompany(CompanyModel company)
    {
        try
        {
            _context.company.Update(company);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public Task<bool> DeleteCompany(int id)
    {
        CompanyModel company = ObterCompanyPorId(id);
        if (company == null)
            return Task.FromResult(false);
        
        company.ativa = false;
        _context.company.Update(company);
        return Task.FromResult(true);
    }

    public List<CompanyModel> ListarCompany()
    {
        return _context.company.Where(c => c.ativa == true).ToList();
    }

    private bool CompanyExists(CompanyModel company)
    {
        CompanyModel DataCompany = _context.company
            .FirstOrDefault(c => c.cnpj == company.cnpj && c.companyName == company.companyName);

        if (DataCompany != null)
            return false;

        return true;
    }
}