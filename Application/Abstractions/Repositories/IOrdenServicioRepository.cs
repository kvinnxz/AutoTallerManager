using Domain.Entities.Taller;
using Domain.Enums;

namespace Application.Abstractions.Repositories;

public interface IOrdenServicioRepository : IGenericRepository<OrdenServicio>
{
    Task<bool> VehiculoTieneOrdenActivaAsync(int vehiculoId, CancellationToken ct = default);
}
