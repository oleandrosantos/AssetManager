using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.Validations
{
    public class DominioInvalidoException : Exception
    {
        public DominioInvalidoException(string msgError) : base (msgError) 
        { }

        public static void When(bool hasError, string msgError)
        {
            if (hasError)
                throw new DominioInvalidoException(msgError);
        }
    }
}
