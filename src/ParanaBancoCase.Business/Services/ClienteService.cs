using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models;
using ParanaBancoCase.Business.Models.Validations;

namespace ParanaBancoCase.Business.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task Adicionar(Cliente cliente)
    {
        if (!Validar(new ClienteValidation(), cliente))
        {
            return;
        }

        if (_clienteRepository.BuscarTodos().Result.Any(x => x.Email == cliente.Email))
        {
            return;
        }

        await _clienteRepository.Adicionar(cliente);
    }

    public async Task Atualizar(Cliente cliente)
    {
        if (!Validar(new ClienteValidation(), cliente))
        {
            return;
        }

        if (_clienteRepository.BuscarTodos().Result.Any(x => x.Email == cliente.Email))
        {
            return;
        }

        await _clienteRepository.Atualizar(cliente);
    }

    public async Task Remover(string email)
    {
        if (_clienteRepository.BuscarTodos().Result.All(x => x.Email != email))
        {
            return;
        }

        await _clienteRepository.Remover(email);
    }



    private bool Validar(ClienteValidation clienteValidacao, Cliente cliente)
    {
        var validacao = clienteValidacao.Validate(cliente);

        if (validacao.IsValid)
        {
            return true;
        }

        return false;
    }

    public void Dispose()
    {
        _clienteRepository?.Dispose();
    }

}