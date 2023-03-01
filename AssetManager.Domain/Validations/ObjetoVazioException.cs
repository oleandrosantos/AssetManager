using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.Validations
{
    public class ObjetoVazioException : Exception
    {
        public ObjetoVazioException(string msgError):base(msgError)
        { }

        public static void When(bool hasError, string msg)
        {
            if (hasError)
                throw new ObjetoVazioException(msg);
        }
    }
}
