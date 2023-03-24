using System.ComponentModel;

namespace AssetManager.Application.Enums
{
    public enum TiposEventos
    {
        [Description("Emprestimo")]
        Emprestimo = 1,
        
        [Description("Fim Emprestimo")]
        FimEmprestimo = 2,

        [Description("Venda")]
        Venda = 3,
        
        [Description("Compra")]
        Compra = 4,
        
        [Description("Substituicao Ativo")]
        Substituicao = 5,
        
        [Description("Delete")]
        Delete = 6
    }
}
