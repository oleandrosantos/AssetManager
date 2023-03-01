using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AssetManager.Application.DTO.Companhia;
public class CriarCompanhiaDTO
{
    public string? NomeCompanhia { get; set; }
    [MinLength(14), StringLength(14)]
    public string? Cnpj { get; set; }
    [DefaultValue(true)]
    public bool Ativa { get; set; }
}


