using AssetManager.Domain.Entities;

namespace AssetManager.Application.DTO.AssetEvents;
public class EventosAtivoDTO
{
    public int IdEventosAtivo { get; set; }
    public int IdAtivo { get; set; }
    public string IdUsuario { get; set; }
    public string IdUsuarioRegistro { get; set; }
    public int TipoEvento { get; set; }
    public DateTime DataEvento { get; set; }
    public string Descricao { get; set; }
}
