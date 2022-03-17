using AssetManager.Model;

namespace AssetManager.ViewModel;

public class LoginResult
{
    public bool logado { get; set; }
    public string mensagem { get; set; }

    public LoginResult(bool resultado, string mensagemLogin)
    {
        logado = resultado;
        mensagem = mensagemLogin;
    }
}