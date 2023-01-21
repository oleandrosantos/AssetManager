using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.DTO
{
    public class ResultOperation
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
    
        public ResultOperation(string message, bool isSucess = false)
        {
            IsSucess= isSucess;
            Message = message;
        }
    
    }
}
