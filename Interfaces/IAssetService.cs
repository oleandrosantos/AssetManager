using AssetManager.Model;
using AssetManager.ViewModel;

namespace AssetManager.Interfaces;
public interface IAssetService
{
    public string Create(CreateAsset asset);

    public string Update(CreateAsset asset);
    public List<AssetModel>? AssetCompanyList(int idCompany);
    public bool DeleteAsset(int idAsset, string exclusionInfo);

}