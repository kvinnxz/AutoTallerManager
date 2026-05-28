using Application.Abstractions;
using Application.Common;
using MediatR;

namespace Application.UseCases.Repuestos.AjustarStock;

public sealed class AjustarStockHandler(IUnitOfWork uow) : IRequestHandler<AjustarStockCommand, Result>
{
    public async Task<Result> Handle(AjustarStockCommand cmd, CancellationToken ct)
    {
        var r = await uow.Repuestos.GetByIdAsync(cmd.RepuestoId, ct);
        if (r is null || !r.IsActive) return Result.Failure("Repuesto no encontrado.");

        var nuevo = r.CantidadStock + cmd.Cantidad;
        if (nuevo < 0) return Result.Failure($"Stock insuficiente. Disponible: {r.CantidadStock}.");

        r.CantidadStock = nuevo;
        r.UpdatedAt     = DateTime.UtcNow;
        uow.Repuestos.Update(r);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
