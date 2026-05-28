using Application.Abstractions;
using Application.Common;
using Domain.Entities.Taller;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.AgregarRepuesto;

public sealed class AgregarRepuestoHandler(IUnitOfWork uow) : IRequestHandler<AgregarRepuestoCommand, Result>
{
    public async Task<Result> Handle(AgregarRepuestoCommand cmd, CancellationToken ct)
    {
        var orden = await uow.OrdenesServicio.GetByIdAsync(cmd.OrdenId, ct);
        if (orden is null) return Result.Failure("Orden no encontrada.");
        if (orden.Estado != EstadoOrden.Pendiente && orden.Estado != EstadoOrden.EnProceso)
            return Result.Failure("Solo se pueden agregar repuestos a órdenes activas.");

        var repuesto = await uow.Repuestos.GetByIdAsync(cmd.RepuestoId, ct);
        if (repuesto is null || !repuesto.IsActive) return Result.Failure("Repuesto no encontrado.");
        if (repuesto.CantidadStock < cmd.Cantidad)
            return Result.Failure($"Stock insuficiente. Disponible: {repuesto.CantidadStock}.");

        var detalle = new DetalleOrden
        {
            OrdenServicioId = cmd.OrdenId,
            RepuestoId      = cmd.RepuestoId,
            Cantidad        = cmd.Cantidad,
            PrecioUnitario  = repuesto.PrecioUnitario
        };

        repuesto.CantidadStock -= cmd.Cantidad;
        repuesto.UpdatedAt      = DateTime.UtcNow;

        await uow.OrdenesServicio.AddAsync(null!, ct); // workaround: add via UoW context
        // Direct EF access via UoW internal context
        uow.Repuestos.Update(repuesto);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
