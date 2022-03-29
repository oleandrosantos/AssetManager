using AssetManager.Model;

namespace AssetManager.ViewModel;

public class CreateUserViewModel
{
    public string? name { get; set; }
    public string? password { get; set; }
    public string email { get; set; }
    public string? role { get; set; }
    public int idCompany { get; set; }
}