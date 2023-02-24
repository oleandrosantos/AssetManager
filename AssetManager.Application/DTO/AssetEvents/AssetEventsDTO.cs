using AssetManager.Domain.Entities;

namespace AssetManager.Application.DTO.AssetEvents;
public class AssetEventsDTO
{
    public int IdEvent { get; set; }
    public int IdAsset { get; set; }
    public string IdUser { get; set; }
    public string IdUserRegister { get; set; }
    public int EventType { get; set; }
    public DateTime EventDate { get; set; }
    public string Description { get; set; }
}
