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