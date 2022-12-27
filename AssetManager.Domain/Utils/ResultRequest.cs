namespace AssetManager.Domain.Utils;
public class ResultRequest
{
    public ResultRequest(bool status, string msg)
    {
        Status = status;
        Mensagem = msg;
    }
    public bool Status { get; set; }
    public string Mensagem { get; set; }
    
    
}