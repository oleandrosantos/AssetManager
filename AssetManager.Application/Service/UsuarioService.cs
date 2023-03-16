using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AssetManager.Domain.Interfaces.Repositorys;
using AssetManager.Application.Interfaces;
using AssetManager.Application.DTO.Usuario;
using AutoMapper;
using AssetManager.Domain.Entities;
using AssetManager.Domain.Validations;
using AssetManager.Application.Helpers;
using AssetManager.Application.DTO.Token;
using System.Security.Claims;

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

    public Task<TokenModel> Login(string email, string password)
    {
        var token = new TokenModel();
        var user = _usuarioRepository.ObterUsuarioPorEmail(email).Result;

        if (user == null || PasswordHelper.VerificandoSenha(user.Password, password))
            throw new Exception();

        token.AcessToken = _tokenService.GerarToken(_mapper.Map<UsuarioDTO>(user));
        token.RefreshToken = ObterRefreshToken(email);
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

    public Task<string> RenovarTokens(string token)
    {
        var claimsToken = _tokenService.ObterClaimsDeTokenExpirado(token);
        var sasa = claimsToken.Claims.Where(a => a.Type == ClaimTypes.Email).FirstOrDefault().Value;
        var sasas = claimsToken.Claims.ToList();
        return Task.FromResult<string>("");
    }

    private string ObterRefreshToken(string email)
    {
        var refreshToken = _tokenService.GerarRefreshToken();
        var usuario = _usuarioRepository.ObterUsuarioPorEmail(email).Result;
        usuario.RefreshToken = refreshToken;
        usuario.DataExpiracaoRefreshToken = _tokenService.ObterDataExpiracaoRefreshToken();
        _usuarioRepository.Atualizar(usuario);
        return refreshToken;
    }
}