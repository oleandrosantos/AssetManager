using Xunit;
using FluentAssertions;
using AssetManager.Domain.Entities;

namespace AssetManager.Tests;

public class CompanyUnitTest
{
    [Fact(DisplayName = "Create Company With Valids Paraments")]
    public void CreateCompany_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new CompanyEntity(1,"Brasil Co", "51192264000125", true);
        action.Should()
            .NotThrow<Domain.Validations.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Company With Invalid Cnpj")]
    public void CreateCompany_WithInvalidCnpj_ResultException()
    {
        Action action = () => new CompanyEntity(1, "Brasil Co", "51192264000101", true);
        action.Should()
            .Throw<Domain.Validations.DomainExceptionValidation>()
            .WithMessage("CNPJ Invalido!");
    }
}