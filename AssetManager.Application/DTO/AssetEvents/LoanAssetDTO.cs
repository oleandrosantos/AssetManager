using AssetManager.Application.DTO.AssetEvents;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Application.DTO.AssetEvents;
public class LoanAssetDTO : AssetEventsDTO
{
    public LoanAssetDTO()
    {
        this.EventType = (int)Enums.EventsType.Loan;
    }

}