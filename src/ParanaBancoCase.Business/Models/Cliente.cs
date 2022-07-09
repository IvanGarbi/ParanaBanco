namespace ParanaBancoCase.Business.Models;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public Cliente(string nome, string email)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
    }
}