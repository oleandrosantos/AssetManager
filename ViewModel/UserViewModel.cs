using AssetManager.Model;

namespace AssetManager.ViewModel;

public class CreateUserViewModel
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    public int IdCompany { get; set; }
}