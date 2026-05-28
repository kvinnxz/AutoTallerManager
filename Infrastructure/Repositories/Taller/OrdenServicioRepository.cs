using Application.Abstractions.Repositories;
using Domain.Entities.Taller;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Taller;

public class OrdenServicioRepository(AppDbContext db)
    : GenericRepository<OrdenServicio>(db), IOrdenServicioRepository
{
    public Task<bool> VehiculoTieneOrdenActivaAsync(int vehiculoId, CancellationToken ct)
        => Set.AnyAsync(o =>
            o.VehiculoId == vehiculoId &&
            (o.Estado == EstadoOrden.Pendiente || o.Estado == EstadoOrden.EnProceso), ct);
}
