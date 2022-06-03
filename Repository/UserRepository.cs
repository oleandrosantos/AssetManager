using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using AutoMapper;

namespace AssetManager.Repository;

public class UserRepository
{
    private DataContext _context;
    private IMapper _mapper;
    private CompanyRepository _companyRepository;
    
    public UserRepository(DataContext context, IMapper mapper, CompanyRepository companyRepository)
    {
        _mapper = mapper;
        _context = context;
        _companyRepository = companyRepository;
    }
    public string Create(CreateUserViewModel dadosUsuario)
    {
        UserModel novoUsuario = _mapper.Map<CreateUserViewModel, UserModel>(dadosUsuario);
        novoUsuario.company = _companyRepository.GetCompanyByID(dadosUsuario.idCompany);
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
            return $"{novoUsuario.company}, {novoUsuario.email}, {novoUsuario.name}" +
                   $"Erro {e.Message}";
        }
    }

    public UserModel? GetUserByEmail(string email) => _context.usuario.FirstOrDefault(k => k.email == email);

    public UserModel? GetUserById(string id) => _context.usuario.Find(id);
    
}