using ParanaBancoCase.Business.Models;

namespace ParanaBancoCase.Business.Interfaces;

public interface IClienteService : IDisposable
{
    Task Adicionar(Cliente cliente);

    Task Atualizar(Cliente cliente);

    Task Remover(string email);

}