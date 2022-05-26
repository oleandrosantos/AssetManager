using AssetManager.Data;
using AssetManager.Model;
using Microsoft.EntityFrameworkCore;

namespace AssetManager.Repository;

public class AssetRepository
{
    private DataContext _context;

    public AssetRepository(DataContext context)
    {
        _context = context;
    }

    public AssetModel? Create(AssetModel asset)
    {
        try
        {
            _context.asset.Add(asset);
            _context.SaveChanges();
            
            return asset;

        }catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }      
    }

    public AssetModel? Update(AssetModel asset)
    {
        try
        {
            if (asset.idAsset == 0 || asset.idAsset == null)
            {
                throw new Exception("Não conseguimos atualizar os dados");
            }
            _context.asset.Update(asset);
            _context.SaveChanges();
            return asset;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public List<AssetModel>? AssetCompanyList(int idCompany)
    {
        var assetCompany = _context.asset.Where(a => a.company.idCompany == idCompany)
            .Include(a => a.company)
            .ToList();

        if(assetCompany.Count == 0 || assetCompany == null)
        {
            return null;
        }
        return assetCompany;
    }
    public bool DeleteAsset(int idAsset, string exclusionInfo)
    {
        AssetModel? asset = _context.asset.Find(idAsset);
        if (asset == null)
        {
            asset.exclusionDate = DateTime.Now;
            asset.exclusionInfos = exclusionInfo;
            _context.asset.Update(asset);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public AssetModel? GetAssetByID(int id)
    {
        return _context.asset.Where(a => a.idAsset == id)
            .Include(a => a.company)
            .FirstOrDefault();
    }
}