namespace AssetManager.Application.DTO.AssetEvents;

public class FimEmprestimoAtivoDTO
{
    public int IdAtivo { get; set; }
    public DateTime? DataEvento { get; set; }
    public string? Descricao { get; set; }
    public string IdUsuarioRegistro { get; set; }
}