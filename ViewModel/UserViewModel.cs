using AssetManager.Model;

namespace AssetManager.ViewModel;

public class UserViewModel
{
    public string? idUsuario { get; set; }
    public string? name { get; set; }
    public string? password { get; set; }
    public string email { get; set; }
    public string? role { get; set; }
    public CompanyModel company { get; set; }
}