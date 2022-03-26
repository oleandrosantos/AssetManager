using AssetManager.Model;
using AssetManager.ViewModel;
using System.Text;
using System.Security.Cryptography;
using AssetManager.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AssetManager.Service;

public class UserService :IUserService
{
    private UserRepository _userRepository;
    private IUserService _userServiceImplementation;
    private const KeyDerivationPrf _HashType = KeyDerivationPrf.HMACSHA1;
    private const int _IterCount = 1000;
    private const int _SubkeyLength = 256 / 8;
    private const int _SaltSize = 128 / 8;

    public UserService(UserRepository repository)
    {
        _userRepository = repository;
    }

    public LoginResult Login(string email, string password)
    {
        var usuario = _userRepository.BuscarUsuarioPorEmail(email);
        if (usuario == null)
        {
            return new LoginResult(false, "Email não identificado em nossa base.");
        }
        if (verificandoSenha(Convert.FromBase64String(usuario.password), password))
        {
            return new LoginResult(true, $"Bem vindo! {usuario.name}, seu toke é {usuario.token}");
        }
        else
        {
            return new LoginResult(false, "Usuario ou senha incorretos");
        }
    }

    public string? Create(UserViewModel dadosUsuario)
    {
        var dados = _userRepository.BuscarUsuarioPorEmail(dadosUsuario.email);

        if (dados != null)
        {
            return null;
        }
        dadosUsuario.password = criandoHashDaSenha(dadosUsuario.password);
        string resultado = _userRepository.Create(dadosUsuario);
        
        return resultado;
    }

    private string criandoHashDaSenha(string senha)
    {

        byte[] salt = new byte[_SaltSize];
        using (var rng = RandomNumberGenerator.Create())  
        {
           rng.GetBytes(salt);
        }
        byte[] subkey = KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: _HashType,
                iterationCount: _IterCount,
                numBytesRequested: _SubkeyLength);
        
        var outputBytes = new byte[1+_SaltSize+_SubkeyLength];
        outputBytes[0] = 0x00;
        Buffer.BlockCopy(salt,0,outputBytes,1,_SaltSize);
        Buffer.BlockCopy(subkey,0,outputBytes,1+_SaltSize,_SubkeyLength);
        
        return Convert.ToBase64String(outputBytes);
    }

    public UserModel? BuscarPorEmail(string email)
    {
        return _userRepository.BuscarUsuarioPorEmail(email);
    }
    private bool verificandoSenha(byte[] hashPassword,string password)
    {
        if (hashPassword.Length != 1 + _SaltSize + _SubkeyLength)
            return false;

        byte[] salt = new byte[_SaltSize];
        Buffer.BlockCopy(hashPassword, 1, salt, 0,salt.Length);

        byte[] expectedSubkey = new byte[_SubkeyLength];
        Buffer.BlockCopy(hashPassword,1+salt.Length, expectedSubkey, 0, expectedSubkey.Length);

        byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, _HashType, _IterCount, _SubkeyLength);

        return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
    }
}