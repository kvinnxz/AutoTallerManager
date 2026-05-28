using FluentValidation;

namespace Application.UseCases.Auth.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    private static readonly string[] RolesValidos = ["Admin", "Mecanico", "Recepcionista"];

    public RegisterValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Correo).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Rol)
            .NotEmpty()
            .Must(r => RolesValidos.Contains(r))
            .WithMessage("Rol debe ser Admin, Mecanico o Recepcionista.");
    }
}
