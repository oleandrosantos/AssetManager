using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.User;
using AutoMapper;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Validations;

namespace AssetManager.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UserService(IUserRepository repository, IMapper mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }

    public Task<string> Create(CreateUserDTO newUser)
    {
        try
        {
            var dadosUsuario = _userRepository.GetUserByEmail(newUser.Email).Result;

            return Task.FromResult("Usuario ja possui cadastro em nossa sistema");
        }
        catch (AggregateException ae)
        {
            string resultado = "Houve um erro, e não foi possivel cadastrar o usuario";
            ae.Handle((x) =>
            {
                if (x is EmptyReturnException)
                {
                    var dadosUsuario = _mapper.Map<UserEntity>(newUser);
                    _userRepository.Create(dadosUsuario);
                    resultado = "Usuario Cadastrado com sucesso!";
                    return true;
                }
                return false;
            });

            return Task.FromResult(resultado);
        }

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
}