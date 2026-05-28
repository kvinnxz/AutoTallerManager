using FluentValidation;
namespace Application.UseCases.Vehiculos.Create;
public class CreateVehiculoValidator : AbstractValidator<CreateVehiculoCommand>
{
    public CreateVehiculoValidator()
    {
        RuleFor(x => x.ClienteId).GreaterThan(0);
        RuleFor(x => x.Marca).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Modelo).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Anio).InclusiveBetween(1900, 2100);
        RuleFor(x => x.VIN).NotEmpty().Length(17);
        RuleFor(x => x.Kilometraje).GreaterThanOrEqualTo(0);
    }
}
