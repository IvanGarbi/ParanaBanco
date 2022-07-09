using System.Linq.Expressions;
using ParanaBancoCase.Business.Models;

namespace ParanaBancoCase.Business.Interfaces;

public interface IClienteRepository : IDisposable
{
    Task Adicionar(Cliente entidade);

    Task<Cliente> BuscarPorEmail(string email);

    Task<List<Cliente>> BuscarTodos();

    Task Atualizar(Cliente entidade);

    Task Remover(string email);

    Task<int> SaveChanges();
}