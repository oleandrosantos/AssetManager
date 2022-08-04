using AssetManager.Data;
using AssetManager.Model;
using AssetManager.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        novoUsuario.Company = _companyRepository.GetCompanyByID(dadosUsuario.IdCompany);
        novoUsuario.IdUsuario = Guid.NewGuid().ToString("N");
        novoUsuario.Token = novoUsuario.IdUsuario;
        try
        {
            _context.usuario.Add(novoUsuario);
            _context.SaveChanges();
            return $"Bem Vindo, {novoUsuario.Name}. Seu Cadastro foi efetuado com sucesso!, Seu Id é {novoUsuario.IdUsuario}";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return $"{novoUsuario.Company}, {novoUsuario.Email}, {novoUsuario.Name}" +
                   $"Erro {e.Message}";
        }
    }


    public UserModel? GetUserByEmail(string email)
    {
        return _context.usuario?.FirstOrDefault(k => k.Email == email);
    }
    
    public UserModel? GetUserById(string id) => _context.usuario.Find(id);
    
    public bool UpdateUser(UserModel dadosUsuario)
    {
        _context.Entry(dadosUsuario).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}