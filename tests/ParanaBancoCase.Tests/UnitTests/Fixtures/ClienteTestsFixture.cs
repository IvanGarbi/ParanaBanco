namespace ParanaBancoCase.Tests.UnitTests.Fixtures;


[CollectionDefinition(nameof(ClienteCollection))]
public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
{ }


public class ClienteTestsFixture : IDisposable
{
    public Business.Models.Cliente CriarClienteValido()
    {
        var cliente = new Business.Models.Cliente(
            "João da Silva",
            "joao_silva@gmail.com");

        return cliente;
    }

    public Business.Models.Cliente CriarClienteInvalido()
    {
        var cliente = new Business.Models.Cliente(
            "",
            "joao_silvagmail.com");

        return cliente;
    }

    public void Dispose()
    {

    }
}