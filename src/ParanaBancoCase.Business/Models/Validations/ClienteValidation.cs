using FluentValidation;

namespace ParanaBancoCase.Business.Models.Validations;

public class ClienteValidation : AbstractValidator<Cliente>
{
    public ClienteValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("A propriedade {PropertyName} precisa ser fornecido.")
            .Length(2, 200)
            .WithMessage("A propriedade {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("A propriedade {PropertyName} precisa ser fornecido.")
            .EmailAddress()
            .WithMessage("A propriedade {PropertyName} está inválido.")
            .MaximumLength(100)
            .WithMessage("O campo {PropertyName} precisa ter menos de {MaxLength} caracteres.");
    }
}