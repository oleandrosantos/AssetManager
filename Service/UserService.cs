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

    public UserService(UserRepository repository)
    {
        _userRepository = repository;
    }

    public LoginResult Login(string email, string password)
    {
        var usuario = _userRepository.BuscarUsuarioPorEmail(email);
        if (verificandoSenha(Encoding.ASCII.GetBytes(usuario.password), password))
        {
            
            return new LoginResult(true, $"Bem vindo! {usuario.name}, seu toke é {usuario.token}");
        }
        else
        {
            return new LoginResult(false, "Usuario ou senha incorretos");
        }
    }

    public string Create(CreateUserViewModel dadosUsuario)
    {
        dadosUsuario.password = criandoHashDaSenha(dadosUsuario.password);
        string resultado = _userRepository.Create(dadosUsuario);
        
        return resultado;
    }

    private string criandoHashDaSenha(string senha)
    {
        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }

        string hashedPassword = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256/8));
        
        return hashedPassword;
    }

    private bool verificandoSenha(byte[] hashPassword,string password)
    {
        const KeyDerivationPrf HashType = KeyDerivationPrf.HMACSHA256;
        const int IterCount = 1000;
        const int SubkeyLength = 256 / 8;
        const int SaltSize = 256 / 8;

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