using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Application.Enums
{
    public enum RolesUsuarios
    {
        [Description("Suporte")]
        Suporte = 100,
        
        [Description("Administrador")]
        Administrador = 70,

        [Description("Funcionario")]
        Funcionario = 10
    }
}
