namespace AssetManager.Domain.Entities;
public class EventosAtivoEntity
{
    public int IdEventosAtivo { get; set; }
    public int IdAtivo { get; set; }
    public string IdUsuario { get; set; }
    public string IdUsuarioRegistro { get; set; }
    public int TipoEvento { get; set; }                       
    public DateTime DataEvento { get; set; }
    public string Descricao { get; set; }
    public AtivoEntity Ativo { get; set; }
    public UsuarioEntity Usuario { get; set; }
    public UsuarioEntity UsuarioRegistro { get; set; }
}
