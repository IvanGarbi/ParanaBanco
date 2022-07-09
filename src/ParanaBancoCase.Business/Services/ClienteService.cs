using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models;
using ParanaBancoCase.Business.Models.Validations;

namespace ParanaBancoCase.Business.Services;

public class ClienteService : BaseService, IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository,
                          INotificador notificador) : base(notificador)
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
            Notificar("Já existe um cliente com este e-mail.");
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

        await _clienteRepository.Atualizar(cliente);
    }

    public async Task Remover(string email)
    {

        await _clienteRepository.Remover(email);
    }



    private bool Validar(ClienteValidation clienteValidacao, Cliente cliente)
    {
        var validacao = clienteValidacao.Validate(cliente);

        if (validacao.IsValid)
        {
            return true;
        }

        Notificar(validacao);

        return false;
    }

    public void Dispose()
    {
        _clienteRepository?.Dispose();
    }

}