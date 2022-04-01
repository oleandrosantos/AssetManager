using AssetManager.Model;
using AssetManager.ViewModel;
using System.Security.Cryptography;
using AssetManager.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Interfaces;

namespace AssetManager.Service;

public class UserService :IUserService
{
    private UserRepository _userRepository;
    private const KeyDerivationPrf HashType = KeyDerivationPrf.HMACSHA1;
    private const int IterCount = 1000;
    private const int SubkeyLength = 256 / 8;
    private const int SaltSize = 128 / 8;

    public UserService(UserRepository repository)
    {
        _userRepository = repository;
    }

    public Result Login(string email, string password)
    {
        var usuario = _userRepository.BuscarUsuarioPorEmail(email);
        if (usuario == null)
        {
            return new Result(false, "Email não identificado em nossa base.");
        }
        if (verificandoSenha(Convert.FromBase64String(usuario.password), password))
        {
            return new Result(true, "");
        }
        else
        {
            return new Result(false, "Usuario ou senha incorretos");
        }
    }

    public string? Create(CreateUserViewModel dadosUsuario)
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
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())  
        {
            rng.GetBytes(salt);
        }
        byte[] subkey = KeyDerivation.Pbkdf2(
            password: senha,
            salt: salt,
            prf: HashType,
            iterationCount: IterCount,
            numBytesRequested: SubkeyLength);
        
        var outputBytes = new byte[1+SaltSize+SubkeyLength];
        outputBytes[0] = 0x00;
        Buffer.BlockCopy(salt,0,outputBytes,1,SaltSize);
        Buffer.BlockCopy(subkey,0,outputBytes,1+SaltSize,SubkeyLength);
        
        return Convert.ToBase64String(outputBytes);
    }

    public UserModel? BuscarPorEmail(string email)
    {
        return _userRepository.BuscarUsuarioPorEmail(email);
    }
    private bool verificandoSenha(byte[] hashPassword,string password)
    {
        if (hashPassword.Length != 1 + SaltSize + SubkeyLength)
            return false;

        byte[] salt = new byte[SaltSize];
        Buffer.BlockCopy(hashPassword, 1, salt, 0,salt.Length);

        byte[] expectedSubkey = new byte[SubkeyLength];
        Buffer.BlockCopy(hashPassword,1+salt.Length, expectedSubkey, 0, expectedSubkey.Length);

        byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, HashType, IterCount, SubkeyLength);

        return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
    }
}