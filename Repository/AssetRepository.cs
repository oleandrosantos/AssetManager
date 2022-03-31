using AssetManager.Data;
using AssetManager.Model;

namespace AssetManager.Repository;

public class AssetRepository
{
    private DataContext _context;
    public AssetModel Create(AssetModel asset)
    {
        _context.asset.Add(asset);
        if (_context.SaveChanges() != 0)
        {
            return asset;
        }

        return null;
    }

    public AssetModel Update(AssetModel asset)
    {
        try
        {
            if (asset.idAsset != 0)
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
}