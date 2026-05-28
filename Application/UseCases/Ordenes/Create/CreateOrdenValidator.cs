using FluentValidation;

namespace Application.UseCases.Ordenes.Create;

public class CreateOrdenValidator : AbstractValidator<CreateOrdenCommand>
{
    public CreateOrdenValidator()
    {
        RuleFor(x => x.VehiculoId).GreaterThan(0);
        RuleFor(x => x.MecanicoId).GreaterThan(0);
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(500);
        RuleFor(x => x.CostoManoObra).GreaterThanOrEqualTo(0);
    }
}
