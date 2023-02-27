using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.User;
using AutoMapper;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Validations;
using AssetManager.Application.Helpers;

namespace AssetManager.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;


    public UserService(IUserRepository repository, IMapper mapper, ITokenService tokenService)
    {
        _userRepository = repository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task Create(CreateUserDTO newUser)
    {
        try
        {
            var dadosUsuario = await _userRepository.GetUserByEmail(newUser.Email);
            if (dadosUsuario != null)
                ObjectAlreadyException.When("Email ja cadastrado no sistema");

            dadosUsuario = _mapper.Map<UserEntity>(newUser);
            dadosUsuario.IdUser = Guid.NewGuid().ToString();
            await _userRepository.Create(dadosUsuario);
        }
        catch(Exception e)
        {
            throw e;
        }
     }

    public Task<string> Login(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email).Result;

        if (user == null || PasswordHelper.VerificandoSenha(user.Password, password))
            throw new Exception();

        string token = _tokenService.GenerateToken(_mapper.Map<UserDTO>(user));

        return Task.FromResult(token);
    }

    public Task<UserDTO?> BuscarPorEmail(string email)
    {
        UserEntity? user = _userRepository.GetUserByEmail(email).Result;

        return Task.FromResult(_mapper.Map<UserDTO?>(user));
    }

    public Task<List<UserDTO?>> GetUsersByIdCompany(int idCompany)
    {
        List<UserEntity?> user = _userRepository.GetUsersByIdCompany(idCompany).Result;

        return Task.FromResult(_mapper.Map<List<UserDTO?>>(user));
    }

    public Task UpdateUser(UpdateUserDTO dadosDoUsuario)
    {
        try
        {
            _userRepository.Update(_mapper.Map<UserEntity>(dadosDoUsuario));
            return Task.CompletedTask;
        }
        catch(Exception e)
        {
            return Task.FromException(e);
        }
    }
}