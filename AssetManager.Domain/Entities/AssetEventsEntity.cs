namespace AssetManager.Domain.Entities;
public class AssetEventsEntity
{
    public int IdEvent { get; set; }
    public int IdAsset { get; set; }
    public string IdUser { get; set; }
    public string IdUserRegister { get; set; }
    public int EventType { get; set; }                       
    public DateTime EventDate { get; set; }
    public string Description { get; set; }
    public AssetEntity Asset { get; set; }
    public UserEntity User { get; set; }
    public UserEntity UserRegister { get; set; }
}
