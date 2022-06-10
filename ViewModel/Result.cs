using AssetManager.Model;

namespace AssetManager.ViewModel;

public class Result
{
    public bool Status { get; set; }
    public string Mensagem { get; set; }

    public Result(bool statusLogin, string mensagemLogin)
    {
        Status = statusLogin;
        Mensagem = mensagemLogin;
    }
}