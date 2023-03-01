using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Ativo;
public class AtualizarAtivoDTO
{
    [Required]
    public int IdAtivo { get; set; }
    public string NomeAtivo { get; set; }
    public int? TaxaDepreciacao { get; set; }
    [Required]
    public ulong PrecoEmCentavos { get; set; }
    [Required]
    public string Sku { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public DateTime DataAquisicao { get; set; }
    public DateTime? DataExclusao { get; set; }
    public string? InformacoesExclusao { get; set; }
    [Required]
    public int IdCompanhia { get; set; }
}
