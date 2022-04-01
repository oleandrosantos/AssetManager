using AssetManager.Model;

namespace AssetManager.ViewModel;

public class Result
{
    public bool status { get; set; }
    public string mensagem { get; set; }

    public Result(bool statusLogin, string mensagemLogin)
    {
        status = statusLogin;
        mensagem = mensagemLogin;
    }
}