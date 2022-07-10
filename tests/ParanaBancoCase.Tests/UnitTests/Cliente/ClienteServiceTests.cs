using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models.Validations;
using ParanaBancoCase.Business.Notificacoes;
using ParanaBancoCase.Business.Services;
using ParanaBancoCase.Data.Context;
using ParanaBancoCase.Data.Repository;
using ParanaBancoCase.Tests.UnitTests.Fixtures;

namespace ParanaBancoCase.Tests.UnitTests.Cliente;

[Collection(nameof(ClienteCollection))]
public class ClienteServiceTests
{
    private readonly ClienteTestsFixture _clienteTestsFixture;

    public ClienteServiceTests(ClienteTestsFixture clienteTestsFixture)
    {
        _clienteTestsFixture = clienteTestsFixture;
    }


    [Fact(DisplayName = "Adicionar Cliente Com Sucesso")]
    [Trait("Categoria", "Cliente Service Tests")]
    public async void ClienteService_Adicionar_ExecutarComSucesso()
    {
        //Arrange  
        var cliente = _clienteTestsFixture.CriarClienteValido();
        var options = new DbContextOptionsBuilder<ParanaBancoDbContext>()
            .UseInMemoryDatabase("ParanaBancoCaseDb1")
            .Options;

        var notificacao = new Mock<Notificador>();


        ClienteRepository clienteRepository = new ClienteRepository(new ParanaBancoDbContext(options));

        // Act
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacao.Object))
        {
            await context.Adicionar(cliente);
        }
        var result = await clienteRepository.BuscarTodos();
        var listaNotificacoes = notificacao.Object.RetornaNotificacaoes();
        var temNotificacao = notificacao.Object.TemNotificacao();

        //Assert  
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Assert.Empty(listaNotificacoes);
        Assert.False(temNotificacao);
    }

    [Fact(DisplayName = "Adicionar Cliente Sem Sucesso")]
    [Trait("Categoria", "Cliente Service Tests")]
    public async void ClienteService_Adicionar_ExecutarSemSucesso()
    {
        //Arrange  
        var cliente = _clienteTestsFixture.CriarClienteInvalido();
        var options = new DbContextOptionsBuilder<ParanaBancoDbContext>()
            .UseInMemoryDatabase("ParanaBancoCaseDb2")
            .Options;

        var notificacao = new Mock<Notificador>();


        ClienteRepository clienteRepository = new ClienteRepository(new ParanaBancoDbContext(options));

        // Act
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacao.Object))
        {
            await context.Adicionar(cliente);
        }
        var result = await clienteRepository.BuscarTodos();
        var listaNotificacoes = notificacao.Object.RetornaNotificacaoes();
        var temNotificacao = notificacao.Object.TemNotificacao();

        //Assert  
        Assert.Empty(result);
        Assert.NotEmpty(listaNotificacoes);
        Assert.Equal(4, listaNotificacoes.Count);
        Assert.True(temNotificacao);
    }


    [Fact(DisplayName = "Atualizar Cliente Com Sucesso")]
    [Trait("Categoria", "Cliente Service Tests")]
    public async void ClienteService_Atualizar_ExecutarComSucesso()
    {
        //Arrange  
        var cliente = _clienteTestsFixture.CriarClienteValido();

        var options = new DbContextOptionsBuilder<ParanaBancoDbContext>()
            .UseInMemoryDatabase("ParanaBancoCaseDb3")
            .Options;

        var notificacao = new Mock<Notificador>();
        var notificacaoAtual = new Mock<Notificador>();

        ClienteRepository clienteRepository = new ClienteRepository(new ParanaBancoDbContext(options));

        // Act
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacao.Object))
        {
            await context.Adicionar(cliente);
        }
        var result = await clienteRepository.BuscarPorEmail(cliente.Email);
        var listaNotificacoes = notificacao.Object.RetornaNotificacaoes();
        var temNotificacao = notificacao.Object.TemNotificacao();

        var clienteAtualizado = cliente;
        clienteAtualizado.Nome = "Paulo";
        clienteAtualizado.Email = "paulo@gmail.com";
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacaoAtual.Object))
        {
            await context.Atualizar(clienteAtualizado);
        }

        var listaNotificacoesAtualizado = notificacaoAtual.Object.RetornaNotificacaoes();
        var resultAtual = await clienteRepository.BuscarPorEmail(clienteAtualizado.Email);
        var temNotificacaoAtual = notificacaoAtual.Object.TemNotificacao();

        //Assert  
        Assert.NotNull(result);
        Assert.NotNull(resultAtual);
        Assert.Equal("Paulo", resultAtual.Nome);
        Assert.Equal("paulo@gmail.com", resultAtual.Email);
        Assert.Empty(listaNotificacoes);
        Assert.Empty(listaNotificacoesAtualizado);
        Assert.False(temNotificacao);
        Assert.False(temNotificacaoAtual);
    }

    [Fact(DisplayName = "Atualizar Cliente Sem Sucesso")]
    [Trait("Categoria", "Cliente Service Tests")]
    public async void ClienteService_Atualizar_ExecutarSemSucesso()
    {
        //Arrange  
        var cliente = _clienteTestsFixture.CriarClienteValido();

        var options = new DbContextOptionsBuilder<ParanaBancoDbContext>()
            .UseInMemoryDatabase("ParanaBancoCaseDb4")
            .Options;

        var notificacao = new Mock<Notificador>();
        var notificacaoAtual = new Mock<Notificador>();

        ClienteRepository clienteRepository = new ClienteRepository(new ParanaBancoDbContext(options));

        // Act
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacao.Object))
        {
            await context.Adicionar(cliente);
        }
        var result = await clienteRepository.BuscarPorEmail(cliente.Email);
        var listaNotificacoes = notificacao.Object.RetornaNotificacaoes();
        var temNotificacao = notificacao.Object.TemNotificacao();

        var clienteAtualizado = cliente;
        clienteAtualizado.Nome = "";
        clienteAtualizado.Email = "";
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacaoAtual.Object))
        {
            await context.Atualizar(clienteAtualizado);
        }

        var listaNotificacoesAtualizado = notificacaoAtual.Object.RetornaNotificacaoes();
        var resultAtual = await clienteRepository.BuscarPorEmail(clienteAtualizado.Email);
        var temNotificacaoAtual = notificacaoAtual.Object.TemNotificacao();

        //Assert  
        Assert.NotNull(result);
        Assert.Null(resultAtual);
        Assert.Empty(listaNotificacoes);
        Assert.NotEmpty(listaNotificacoesAtualizado);
        Assert.Equal(4, listaNotificacoesAtualizado.Count);
        Assert.False(temNotificacao);
        Assert.True(temNotificacaoAtual);
    }


    [Fact(DisplayName = "Excluir Cliente Com Sucesso")]
    [Trait("Categoria", "Cliente Service Tests")]
    public async void ClienteService_Excluir_ExecutarComSucesso()
    {
        //Arrange  
        var cliente = _clienteTestsFixture.CriarClienteValido();

        var options = new DbContextOptionsBuilder<ParanaBancoDbContext>()
            .UseInMemoryDatabase("ParanaBancoCaseDb5")
            .Options;

        var notificacao = new Mock<Notificador>();
        var notificacaoAtual = new Mock<Notificador>();

        ClienteRepository clienteRepository = new ClienteRepository(new ParanaBancoDbContext(options));

        // Act
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacao.Object))
        {
            await context.Adicionar(cliente);
        }
        var result = await clienteRepository.BuscarPorEmail(cliente.Email);
        var listaNotificacoes = notificacao.Object.RetornaNotificacaoes();
        var temNotificacao = notificacao.Object.TemNotificacao();

        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacaoAtual.Object))
        {
            await context.Remover(result.Email);
        }

        var listaNotificacoesAtualizado = notificacaoAtual.Object.RetornaNotificacaoes();
        var resultAtual = await clienteRepository.BuscarTodos();
        var temNotificacaoAtual = notificacaoAtual.Object.TemNotificacao();

        //Assert  
        Assert.NotNull(result);
        Assert.Empty(resultAtual);
        Assert.Empty(listaNotificacoes);
        Assert.Empty(listaNotificacoesAtualizado);
        Assert.False(temNotificacao);
        Assert.False(temNotificacaoAtual);
    }

    [Fact(DisplayName = "Excluir Cliente Sem Sucesso")]
    [Trait("Categoria", "Cliente Service Tests")]
    public async void ClienteService_Excluir_ExecutarSemSucesso()
    {
        //Arrange  
        var cliente = _clienteTestsFixture.CriarClienteValido();

        var options = new DbContextOptionsBuilder<ParanaBancoDbContext>()
            .UseInMemoryDatabase("ParanaBancoCaseDb6")
            .Options;

        var notificacao = new Mock<Notificador>();
        var notificacaoAtual = new Mock<Notificador>();

        ClienteRepository clienteRepository = new ClienteRepository(new ParanaBancoDbContext(options));

        // Act
        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacao.Object))
        {
            await context.Adicionar(cliente);
        }
        var result = await clienteRepository.BuscarPorEmail(cliente.Email);
        var listaNotificacoes = notificacao.Object.RetornaNotificacaoes();
        var temNotificacao = notificacao.Object.TemNotificacao();

        using (var context = new ClienteService(new ClienteRepository(new ParanaBancoDbContext(options)), notificacaoAtual.Object))
        {
            await context.Remover("faluno@gmail.com");
        }

        var listaNotificacoesAtualizado = notificacaoAtual.Object.RetornaNotificacaoes();
        var resultAtual = await clienteRepository.BuscarTodos();
        var temNotificacaoAtual = notificacaoAtual.Object.TemNotificacao();

        //Assert  
        Assert.NotNull(result);
        Assert.NotEmpty(resultAtual);
        Assert.Empty(listaNotificacoes);
        Assert.NotEmpty(listaNotificacoesAtualizado);
        Assert.Equal(1, listaNotificacoesAtualizado.Count);
        Assert.False(temNotificacao);
        Assert.True(temNotificacaoAtual);
    }

}