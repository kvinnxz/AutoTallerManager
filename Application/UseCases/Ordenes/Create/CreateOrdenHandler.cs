using Application.Abstractions;
using Application.Common;
using Domain.Entities.Taller;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.Create;

public sealed class CreateOrdenHandler(IUnitOfWork uow) : IRequestHandler<CreateOrdenCommand, Result<int>>
{
    private static readonly Dictionary<TipoServicio, int> DiasEstimados = new()
    {
        { TipoServicio.Diagnostico,             1 },
        { TipoServicio.MantenimientoPreventivo, 2 },
        { TipoServicio.Reparacion,              5 }
    };

    public async Task<Result<int>> Handle(CreateOrdenCommand cmd, CancellationToken ct)
    {
        if (!await uow.Vehiculos.ExistsAsync(v => v.Id == cmd.VehiculoId && v.IsActive, ct))
            return Result<int>.Failure($"Vehículo {cmd.VehiculoId} no encontrado.");

        if (!await uow.Usuarios.TieneRolAsync(cmd.MecanicoId, "Mecanico", ct))
            return Result<int>.Failure($"El usuario {cmd.MecanicoId} no es mecánico.");

        if (await uow.OrdenesServicio.VehiculoTieneOrdenActivaAsync(cmd.VehiculoId, ct))
            return Result<int>.Failure("El vehículo ya tiene una orden activa.");

        var orden = new OrdenServicio
        {
            VehiculoId           = cmd.VehiculoId,
            MecanicoId           = cmd.MecanicoId,
            TipoServicio         = cmd.TipoServicio,
            Descripcion          = cmd.Descripcion,
            CostoManoObra        = cmd.CostoManoObra,
            Estado               = EstadoOrden.Pendiente,
            FechaIngreso         = DateTime.UtcNow,
            FechaEstimadaEntrega = DateTime.UtcNow.AddDays(DiasEstimados[cmd.TipoServicio])
        };

        await uow.OrdenesServicio.AddAsync(orden, ct);
        await uow.SaveChangesAsync(ct);
        return Result<int>.Success(orden.Id);
    }
}
