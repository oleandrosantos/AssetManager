using System.ComponentModel.DataAnnotations;

namespace AssetManager.Infra.Data.DTO.Asset;
public class UpdateAssetDTO : AssetDTO
{
    public int IdAsset { get; set; }
}
