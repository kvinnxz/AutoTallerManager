using Application.Abstractions;
using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.Cancelar;

public sealed class CancelarOrdenHandler(IUnitOfWork uow) : IRequestHandler<CancelarOrdenCommand, Result>
{
    public async Task<Result> Handle(CancelarOrdenCommand cmd, CancellationToken ct)
    {
        var orden = await uow.OrdenesServicio.FirstOrDefaultAsync(
            o => o.Id == cmd.OrdenId, ct, "Detalles.Repuesto");

        if (orden is null) return Result.Failure("Orden no encontrada.");
        if (orden.Estado == EstadoOrden.Completada)
            return Result.Failure("No se puede cancelar una orden completada.");

        foreach (var d in orden.Detalles)
        {
            d.Repuesto.CantidadStock += d.Cantidad;
            uow.Repuestos.Update(d.Repuesto);
        }

        orden.Estado    = EstadoOrden.Cancelada;
        orden.UpdatedAt = DateTime.UtcNow;
        uow.OrdenesServicio.Update(orden);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
