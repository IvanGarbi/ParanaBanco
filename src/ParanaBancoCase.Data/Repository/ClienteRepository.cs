using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ParanaBancoCase.Business.Interfaces;
using ParanaBancoCase.Business.Models;
using ParanaBancoCase.Data.Context;

namespace ParanaBancoCase.Data.Repository;

public class ClienteRepository : IClienteRepository
{
    protected readonly ParanaBancoDbContext Db;
    protected readonly DbSet<Cliente> DbSet;

    public ClienteRepository(ParanaBancoDbContext db)
    {
        Db = db;
        DbSet = db.Set<Cliente>();
    }

    public async Task Adicionar(Cliente entidade)
    {
        DbSet.Add(entidade);
        await SaveChanges();
    }

    public async Task<Cliente> BuscarPorEmail(string email)
    {
        return await DbSet.SingleAsync(x => x.Email == email);
    }

    public async Task<List<Cliente>> BuscarTodos()
    {
        return await DbSet.ToListAsync();
    }

    public async Task Atualizar(Cliente entidade)
    {
        DbSet.Update(entidade);
        await SaveChanges();
    }

    public async Task Remover(string email)
    {
        DbSet.Remove(await DbSet.SingleAsync(x => x.Email == email));
        await SaveChanges();
    }


    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public async void Dispose()
    {
        Db?.Dispose();
    }
}