namespace AssetManager.Domain.Entities;
public class AssetEventsEntity
{
    public int IdEvent { get; set; }
    public int IdAsset { get; set; }
    public int EventType { get; set; }
    public DateTime EventDate { get; set; }
    public string Description { get; set; }
    public virtual AssetEntity Asset { get; set; }
}
