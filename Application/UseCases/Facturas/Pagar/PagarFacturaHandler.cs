using Application.Abstractions;
using Application.Common;
using MediatR;

namespace Application.UseCases.Facturas.Pagar;

public sealed class PagarFacturaHandler(IUnitOfWork uow) : IRequestHandler<PagarFacturaCommand, Result>
{
    public async Task<Result> Handle(PagarFacturaCommand cmd, CancellationToken ct)
    {
        var factura = await uow.Facturas.GetByIdAsync(cmd.FacturaId, ct);
        if (factura is null) return Result.Failure("Factura no encontrada.");
        if (factura.Pagada) return Result.Failure("La factura ya está pagada.");

        factura.Pagada    = true;
        factura.UpdatedAt = DateTime.UtcNow;
        uow.Facturas.Update(factura);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
