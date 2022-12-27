using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Domain.Utils;
using AssetManager.Domain.DTO;
using AssetManager.Domain.Interfaces.Application;

namespace AssetManager.Application.Service;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private const KeyDerivationPrf HashType = KeyDerivationPrf.HMACSHA1;
    private const int ITER_COUNT = 1000;
    private const int SUBKEY_LENGTH = 256 / 8;
    private const int SALT_SIZE = 128 / 8;

    public UserService(IUserRepository repository)
    {
        _userRepository = repository;
    }

    public ResultRequest Login(string email, string password)
    {
        var usuario = _userRepository.GetUserByEmail(email);

        if (usuario == null)
            return new ResultRequest(false, "Email não identificado em nossa base.");
        if (!usuario.isActive)
            return new ResultRequest(false, "Voce não tem permissão para acessar o sistema.");
        if (verificandoSenha(Convert.FromBase64String(usuario.Password), password))
            return new ResultRequest(true, "");
        else
            return new ResultRequest(false, "Usuario ou senha incorretos");

    }

    public string Create(CreateUserDTO dadosUsuario)
    {
        var dados = _userRepository.GetUserByEmail(dadosUsuario.Email);

        if (dados != null)
            return "";
        
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

    public UserDTO? BuscarPorEmail(string email)
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

    public bool UpdateUser(UpdateUserDTO dadosDoUsuario) => _userRepository.Update(dadosDoUsuario);

}