using Domain.Entities.Taller;

namespace Application.Abstractions.Repositories;

public interface IVehiculoRepository : IGenericRepository<Vehiculo>
{
    Task<Vehiculo?> GetByVinAsync(string vin, CancellationToken ct = default);
}
