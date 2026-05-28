using FluentValidation;

namespace Application.UseCases.Clientes.Create;

public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Telefono).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Correo).NotEmpty().EmailAddress().MaximumLength(150);
    }
}
