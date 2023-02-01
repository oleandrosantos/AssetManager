using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.User;
using AutoMapper;
using AssetManager.Domain.Entities;

namespace AssetManager.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private const KeyDerivationPrf HashType = KeyDerivationPrf.HMACSHA1;
    private const int ITER_COUNT = 1000;
    private const int SUBKEY_LENGTH = 256 / 8;
    private const int SALT_SIZE = 128 / 8;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }

    public Task<string> Create(CreateUserDTO newUser)
    {
        var dadosUsuario = _userRepository.GetUserByEmail(newUser.Email).Result;

        if (dadosUsuario != null)
            return Task.FromResult("");

        dadosUsuario.Password = criandoHashDaSenha(dadosUsuario.Password);
        _userRepository.Create(dadosUsuario);

        return Task.FromResult("Usuario Cadastrdo com sucesso!");
    }

    Task<UserDTO> IUserService.Login(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);

        return Task.FromResult(_mapper.Map<UserDTO>(user.Result));
    }

    Task<UserDTO?> IUserService.BuscarPorEmail(string email)
    {
        UserEntity? user = _userRepository.GetUserByEmail(email).Result;

        return Task.FromResult(_mapper.Map<UserDTO?>(user));
    }

    public Task<bool> UpdateUser(UpdateUserDTO dadosDoUsuario)
    {
        try
        {
            _userRepository.Update(_mapper.Map<UserEntity>(dadosDoUsuario));
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);

        }
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
}