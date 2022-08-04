using AssetManager.Model;
using AssetManager.ViewModel;
using System.Security.Cryptography;
using AssetManager.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Interfaces;

namespace AssetManager.Service;

public class UserService : IUserService
{
    private UserRepository _userRepository;
    private const KeyDerivationPrf HashType = KeyDerivationPrf.HMACSHA1;
    private const int ITER_COUNT = 1000;
    private const int SUBKEY_LENGTH = 256 / 8;
    private const int SALT_SIZE = 128 / 8;

    public UserService(UserRepository repository)
    {
        _userRepository = repository;
    }

    public Result Login(string email, string password)
    {
        var usuario = _userRepository.GetUserByEmail(email);

        if (usuario == null)
            return new Result(false, "Email não identificado em nossa base.");
        if (!usuario.isActive)
            return new Result(false, "Voce não tem permissão para acessar o sistema.");
        if (verificandoSenha(Convert.FromBase64String(usuario.Password), password))
            return new Result(true, "");
        else
            return new Result(false, "Usuario ou senha incorretos");

    }

    public string? Create(CreateUserViewModel dadosUsuario)
    {
        var dados = _userRepository.GetUserByEmail(dadosUsuario.Email);

        if (dados != null)
            return null;
        
        dadosUsuario.Password = criandoHashDaSenha(dadosUsuario.Password);
        return _userRepository.Create(dadosUsuario);
    }

    private string criandoHashDaSenha(string senha)
    {
        byte[] salt = new byte[SALT_SIZE];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        byte[] subkey = KeyDerivation.Pbkdf2(
            password: senha,
            salt: salt,
            prf: HashType,
            iterationCount: ITER_COUNT,
            numBytesRequested: SUBKEY_LENGTH);

        var outputBytes = new byte[1 + SALT_SIZE + SUBKEY_LENGTH];
        outputBytes[0] = 0x00;
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SALT_SIZE);
        Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SALT_SIZE, SUBKEY_LENGTH);

        return Convert.ToBase64String(outputBytes);
    }

    public UserModel? BuscarPorEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }
    private bool verificandoSenha(byte[] hashPassword, string password)
    {
        if (hashPassword.Length != 1 + SALT_SIZE + SUBKEY_LENGTH)
            return false;

        byte[] salt = new byte[SALT_SIZE];
        Buffer.BlockCopy(hashPassword, 1, salt, 0, salt.Length);

        byte[] expectedSubkey = new byte[SUBKEY_LENGTH];
        Buffer.BlockCopy(hashPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

        byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, HashType, ITER_COUNT, SUBKEY_LENGTH);

        return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
    }

    public bool UpdateUser(UserModel dadosDoUsuario) => _userRepository.UpdateUser(dadosDoUsuario);

}