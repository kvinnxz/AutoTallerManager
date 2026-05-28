using Application.Abstractions;
using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Clientes.Delete;

public sealed class DeleteClienteHandler(IUnitOfWork uow) : IRequestHandler<DeleteClienteCommand, Result>
{
    public async Task<Result> Handle(DeleteClienteCommand cmd, CancellationToken ct)
    {
        var cliente = await uow.Clientes.GetByIdAsync(cmd.Id, ct);
        if (cliente is null || !cliente.IsActive) return Result.Failure("Cliente no encontrado.");

        bool ordenesActivas = await uow.OrdenesServicio.ExistsAsync(
            o => o.Vehiculo.ClienteId == cmd.Id &&
                 (o.Estado == EstadoOrden.Pendiente || o.Estado == EstadoOrden.EnProceso), ct);

        if (ordenesActivas) return Result.Failure("El cliente tiene órdenes activas.");

        cliente.IsActive  = false;
        cliente.UpdatedAt = DateTime.UtcNow;
        uow.Clientes.Update(cliente);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
