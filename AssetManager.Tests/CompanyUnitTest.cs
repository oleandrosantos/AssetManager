using Xunit;
using FluentAssertions;
using AssetManager.Domain.Entities;

namespace AssetManager.Tests;

public class CompanhiaUnitTest
{
    [Fact(DisplayName = "Criar Companhia com parametros validos")]
    public void CriarCompanhia_CriarCompanhiaComParametrosValidos_ResultadoSucesso()
    {
        Action action = () => new CompanhiaEntity(1,"Brasil Co", "51192264000125", true);
        action.Should()
            .NotThrow<Domain.Validations.DominioInvalidoException>();
    }

    [Fact(DisplayName = "Criar Companhia com Cnpj invalido")]
    public void CriarCompanhia_CriarCompanhiaComCnpjInvalid_ResultadoDominioInvalidoException()
    {
        Action action = () => new CompanhiaEntity(1, "Brasil Co", "51192264000101", true);
        action.Should()
            .Throw<Domain.Validations.DominioInvalidoException>()
            .WithMessage("CNPJ Invalido!");
    }
}