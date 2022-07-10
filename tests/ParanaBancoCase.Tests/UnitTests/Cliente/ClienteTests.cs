using Moq;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models.Validations;
using ParanaBancoCase.Business.Services;
using ParanaBancoCase.Tests.UnitTests.Fixtures;

namespace ParanaBancoCase.Tests.UnitTests.Cliente;

[Collection(nameof(ClienteCollection))]
public class ClienteTests
{
    private readonly ClienteTestsFixture _clienteTestsFixture;

    public ClienteTests(ClienteTestsFixture clienteTestsFixture)
    {
        _clienteTestsFixture = clienteTestsFixture;
    }

    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Cliente Fixture Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = _clienteTestsFixture.CriarClienteValido();

        // Act
        var result = new ClienteValidation().Validate(cliente);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Cliente Fixture Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = _clienteTestsFixture.CriarClienteInvalido();

        // Act
        var result = new ClienteValidation().Validate(cliente);

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(4, result.Errors.Count);
    }
}