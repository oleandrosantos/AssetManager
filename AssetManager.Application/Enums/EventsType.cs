using System.ComponentModel;

namespace AssetManager.Application.Enums
{
    public enum EventsType
    {
        [Description("Loan")]
        Loan = 1,
        [Description("Terminate")]
        Terminate = 2,
        [Description("Sell")]
        Sell = 3,
        [Description("Buy")]
        Buy = 4,
        [Description("Replace")]
        Replace = 5,
        [Description("Delete")]
        Delete = 6
    }
}
