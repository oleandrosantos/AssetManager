using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using AutoMapper;

namespace AssetManager.Repository;

public class UserRepository
{
    private DataContext _context;
    private IMapper _mapper;
    
    public UserRepository(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public string Create(CreateUserViewModel dadosUsuario)
    {
        UserModel novoUsuario = _mapper.Map<UserModel>(dadosUsuario);
        novoUsuario.idUsuario = Guid.NewGuid().ToString("N");
        novoUsuario.token = novoUsuario.idUsuario;
        try
        {
            _context.usuario.Add(novoUsuario);
            _context.SaveChanges();
            return $"Bem Vindo, {novoUsuario.name }. Seu Cadastro foi efetuado com sucesso!, Seu Id é {novoUsuario.idUsuario}";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return $"Erro {e.Message}";
        }
    }

    public UserModel? BuscarUsuarioPorEmail(string email)
    {
        UserModel usuario = _context.usuario.FirstOrDefault(k => k.email == email);
        return usuario;
    }
}