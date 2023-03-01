using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManager.Domain.Entities;

public class AtivoEntity
{
    public int IdAtivo { get; set; }
    public string Sku { get; set; }
    public string NomeAtivo { get; set; }
    public int TaxaDepreciacao { get; set; }
    public ulong PrecoEmCentavos { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DataAquisicao { get; set; }
    public DateTime? DataExclusao { get; set; }
    public string? InformacoesExclusao { get; set; }
    public int IdCompanhia { get; set; }
    public CompanhiaEntity Companhia { get; set; }
    public ICollection<EventosAtivoEntity> EventosAtivo { get; set; }
}