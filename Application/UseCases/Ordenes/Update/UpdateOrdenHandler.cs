using Application.Abstractions;
using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.Update;

public sealed class UpdateOrdenHandler(IUnitOfWork uow) : IRequestHandler<UpdateOrdenCommand, Result>
{
    public async Task<Result> Handle(UpdateOrdenCommand cmd, CancellationToken ct)
    {
        var orden = await uow.OrdenesServicio.GetByIdAsync(cmd.Id, ct);
        if (orden is null) return Result.Failure("Orden no encontrada.");
        if (orden.Estado == EstadoOrden.Completada || orden.Estado == EstadoOrden.Cancelada)
            return Result.Failure("No se puede modificar una orden cerrada.");

        if (cmd.Estado              is not null) orden.Estado               = cmd.Estado.Value;
        if (cmd.TrabajoRealizado    is not null) orden.TrabajoRealizado      = cmd.TrabajoRealizado;
        if (cmd.CostoManoObra       is not null) orden.CostoManoObra        = cmd.CostoManoObra.Value;
        if (cmd.FechaEstimadaEntrega is not null) orden.FechaEstimadaEntrega = cmd.FechaEstimadaEntrega;
        if (orden.Estado == EstadoOrden.Completada) orden.FechaEntregaReal = DateTime.UtcNow;

        orden.UpdatedAt = DateTime.UtcNow;
        uow.OrdenesServicio.Update(orden);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
