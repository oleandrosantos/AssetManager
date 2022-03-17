using AssetManager.Data;
using AssetManager.Model;
using AssetManager.Profile;
using AssetManager.ViewModel;
using AutoMapper;
using System.Security.Cryptography;

namespace AssetManager.Service;

public class UserService :IUserService
{
    private DataContext _context;
    private IMapper _mapper;
    private IUserService _userServiceImplementation;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public string Create(CreateUserViewModel dadosUsuario)
    {
        UserModel novoUsuario = _mapper.Map<UserModel>(dadosUsuario);
        novoUsuario.idUsuario = Guid.NewGuid().ToString("N");
        novoUsuario.token = novoUsuario.idUsuario;
        _context.usuario.Add(novoUsuario);
        _context.SaveChanges();
        return $"usuario {novoUsuario.name} cadastrado com sucesso";
    }

    private string criandoHashDaSenha(string senha)
    {
        
    }
}