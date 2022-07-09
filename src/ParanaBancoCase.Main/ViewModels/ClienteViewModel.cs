using System.ComponentModel.DataAnnotations;

namespace ParanaBancoCase.Main.ViewModels;

public class ClienteViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
    [StringLength(100, ErrorMessage = "O campo {0} não pode passar de {1} caracteres.")]
    public string Email { get; set; }
}