using AssetManager.Model;

namespace AssetManager.ViewModel;

public class Result
{
    public bool status { get; set; }
    public string mensagem { get; set; }

    public Result(bool status, string mensagemLogin)
    {
        status = status;
        mensagem = mensagemLogin;
    }
}