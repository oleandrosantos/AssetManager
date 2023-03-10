using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.Usuario;
using AutoMapper;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Validations;
using AssetManager.Application.Helpers;

namespace AssetManager.Application.Service;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;


    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task CadastrarUsuario(CriarUsuarioDTO newUser)
    {
        try
        {
            var dadosUsuario = await _usuarioRepository.ObterUsuarioPorEmail(newUser.Email);
            if (dadosUsuario != null)
                ObjetoCadastradoException.When("Email ja cadastrado no sistema");

            dadosUsuario = _mapper.Map<UsuarioEntity>(newUser);
            dadosUsuario.IdUsuario = Guid.NewGuid().ToString();
            await _usuarioRepository.Cadastrar(dadosUsuario);
        }
        catch(Exception e)
        {
            throw e;
        }
     }

    public Task<string> Login(string email, string password)
    {
        var user = _usuarioRepository.ObterUsuarioPorEmail(email).Result;

        if (user == null || PasswordHelper.VerificandoSenha(user.Password, password))
            throw new Exception();

        string token = _tokenService.GerarToken(_mapper.Map<UsuarioDTO>(user));

        return Task.FromResult(token);
    }

    public Task<UsuarioDTO?> BuscarPorEmail(string email)
    {
        UsuarioEntity? user = _usuarioRepository.ObterUsuarioPorEmail(email).Result;

        return Task.FromResult(_mapper.Map<UsuarioDTO?>(user));
    }

    public Task<List<UsuarioDTO?>> ObterTodosOsUsuarioDaCompanhia(int IdCompanhia)
    {
        List<UsuarioEntity?> user = _usuarioRepository.ObterUsuariosDaCompanhia(IdCompanhia).Result;

        return Task.FromResult(_mapper.Map<List<UsuarioDTO?>>(user));
    }

    public Task AtualizarUsuario(AtualizarUsuarioDTO dadosDoUsuario)
    {
        try
        {
            _usuarioRepository.Atualizar(_mapper.Map<UsuarioEntity>(dadosDoUsuario));
            return Task.CompletedTask;
        }
        catch(Exception e)
        {
            return Task.FromException(e);
        }
    }

    public Task RevogarAcessoUsuario(string email)
    {
        return _usuarioRepository.RevogarAcessoUsuario(email);
    }
}