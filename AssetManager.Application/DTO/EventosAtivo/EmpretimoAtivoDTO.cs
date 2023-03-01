using AssetManager.Application.DTO.AssetEvents;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.AssetEvents;
public class EmpretimoAtivoDTO : EventosAtivoDTO
{
    public EmpretimoAtivoDTO()
    {
        this.TipoEvento = (int)Enums.TiposEventos.Emprestimo;
    }

}