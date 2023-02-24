using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
