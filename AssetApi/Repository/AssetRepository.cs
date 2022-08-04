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
            if (asset.IdAsset == 0 || asset.IdAsset == null)
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

    public async Task<List<AssetModel>> AssetCompanyList(int idCompany)
    {
        var assetCompany = _context.asset.Where(a => a.Company.IdCompany == idCompany)
            .Include(a => a.Company)
            .ToListAsync();

        return await assetCompany;
    }
    public bool DeleteAsset(int idAsset, string exclusionInfo)
    {
        AssetModel? asset = _context.asset.Find(idAsset);
        if (asset == null)
        {
            asset.ExclusionDate = DateTime.Now;
            asset.ExclusionInfos = exclusionInfo;
            _context.asset.Update(asset);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public AssetModel? GetAssetByID(int id)
    {
        return _context.asset.Where(a => a.IdAsset == id)
            .Include(a => a.Company)
            .FirstOrDefault();
    }
}