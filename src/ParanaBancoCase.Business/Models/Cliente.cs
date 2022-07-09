namespace ParanaBancoCase.Business.Models;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public Cliente(Guid id, string nome, string email)
    {
        Id = id;
        Nome = nome;
        Email = email;
    }
}