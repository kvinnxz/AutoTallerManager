using Domain.Entities.Taller;

namespace Application.Abstractions.Repositories;

public interface IRepuestoRepository : IGenericRepository<Repuesto>
{
    Task<Repuesto?> GetByCodigoAsync(string codigo, CancellationToken ct = default);
}
