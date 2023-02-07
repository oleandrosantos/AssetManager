using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.Validations
{
    public class EmptyReturnException : Exception
    {
        public EmptyReturnException(string msgError):base(msgError)
        { }

        public static void When(bool hasError, string msg)
        {
            if (hasError)
                throw new EmptyReturnException(msg);
        }
    }
}
