using AssetManager.Data;
using AssetManager.Model;

namespace AssetManager.Service;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public string Create(UserModel newUser)
    {
        _context.usuario.Add(newUser);
        _context.SaveChanges();
        return $"usuario {newUser.name} cadastrado com sucesso";
    } 
}