using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Domain.Validations
{
    public class ObjetoCadastradoException : Exception
    {
        public ObjetoCadastradoException(string msgError) : base(msgError) { }

        public static void When(string msg)
        {
            throw new ObjetoCadastradoException(msg);
        }
    }
}
