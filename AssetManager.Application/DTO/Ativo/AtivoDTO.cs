using AssetManager.Application.DTO.AssetEvents;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.Ativo;
public class AtivoDTO
{
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
    public List<EventosAtivoDTO> EventosAtivo { get; set; }



    public bool ELocavel()
    {
        var ultimoContratoAtivo = EventosAtivo.Where(a => a.TipoEvento == (int)Enums.TiposEventos.Emprestimo).OrderBy(a => a.DataEvento).LastOrDefault();
        return EventosAtivo.Where(a => a.DataEvento > ultimoContratoAtivo.DataEvento && a.TipoEvento == (int)Enums.TiposEventos.FimEmprestimo).Any();
    }
}
