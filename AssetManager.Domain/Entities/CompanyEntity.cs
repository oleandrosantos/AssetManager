using AssetManager.Domain.Validations;

namespace AssetManager.Domain.Entities;

public class CompanyEntity
{
    private string _cnpj { get; set; }
    public int IdCompany { get; set; }
    public string? CompanyName { get; set; }
    public string Cnpj
    { 
        get => _cnpj; 
        set
        {
            if(Maoli.Cnpj.Validate(value))
                _cnpj = value;
            else
                DomainExceptionValidation.When(true, "CNPJ Invalido!");
        }
    }
    public bool IsAtiva { get; set; }

    public CompanyEntity(int id, string name, string cnpj, bool isAtiva)
    {
        IdCompany = id;
        CompanyName = name;
        Cnpj = cnpj;
        IsAtiva = isAtiva;
    }
    public CompanyEntity(string name, string cnpj, bool isAtiva)
    {
        CompanyName = name;
        Cnpj = cnpj;
        IsAtiva = isAtiva;
    }
    public ICollection<UserEntity> Users { get; set; }
    public ICollection<AssetEntity> Asset { get; set; }
    public ICollection<LoanAssetEntity> Loans { get; set; }

}