using AssetManager.Domain.Validations;

namespace AssetManager.Domain.Entities;

public class CompanhiaEntity
{
    private string _cnpj { get; set; }
    public int IdCompanhia { get; set; }
    public string? NomeCompanhia { get; set; }
    public string Cnpj
    { 
        get => _cnpj; 
        set
        {
            if(Maoli.Cnpj.Validate(value))
                _cnpj = value;
            else
                DominioInvalidoException.When(true, "CNPJ Invalido!");
        }
    }
    public bool Ativa { get; set; }

    public DateTime? ExclusionDate { get; set; }

    public CompanhiaEntity() { }

    public CompanhiaEntity(int id, string Nome, string cnpj, bool Ativa)
    {
        IdCompanhia = id;
        NomeCompanhia = Nome;
        Cnpj = cnpj;
        Ativa = Ativa;
    }
    public CompanhiaEntity(string Nome, string cnpj, bool Ativa)
    {
        NomeCompanhia = Nome;
        Cnpj = cnpj;
        Ativa = Ativa;
    }
    public ICollection<UsuarioEntity> Users { get; set; }
    public ICollection<AtivoEntity> Asset { get; set; }
}