using FluentValidation;
namespace Application.UseCases.Repuestos.Create;
public class CreateRepuestoValidator : AbstractValidator<CreateRepuestoCommand>
{
    public CreateRepuestoValidator()
    {
        RuleFor(x => x.Codigo).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Categoria).NotEmpty().MaximumLength(80);
        RuleFor(x => x.CantidadStock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.StockMinimo).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PrecioUnitario).GreaterThan(0);
    }
}
