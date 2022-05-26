using AssetManager.Data;
using AssetManager.Interfaces;
using AssetManager.Model;
using AssetManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Repository;

public class CompanyRepository
{
    private DataContext _context;

    public CompanyRepository(DataContext context)
    {
        _context = context;
    }

    public CompanyModel? GetCompanyByID(int idCompany)
    {
        return _context.company.Find(idCompany);
    }

    public CompanyModel? CreateCompany(CompanyModel company)
    {
        if (CompanyExists(company))
        {
            return null;
        }

        _context.company.Add(company);
        _context.SaveChanges();
        return company;
    }

    public bool UpdateCompany(CompanyModel company)
    {
        try
        {
            _context.company.Update(company);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> DeleteCompany(int id)
    {
        CompanyModel? company = GetCompanyByID(id);
        if (company == null)
            return Task.FromResult(false);

        company.ativa = false;
        if(!UpdateCompany(company))
            return Task.FromResult(false);
        
        return Task.FromResult(true);
    }

    public Task<List<CompanyModel>> CompanyList()
    {
        return Task.FromResult(_context.company.Where(c => c.ativa == true).ToList());
    }

    private bool CompanyExists(CompanyModel company)
    {
        CompanyModel? DataCompany = _context.company
            .FirstOrDefault(c => c.cnpj == company.cnpj);

        if (DataCompany == null)
            return false;

        return true;
    }
}