using Domain.Entities.Taller;

namespace Application.Abstractions.Repositories;

public interface IFacturaRepository : IGenericRepository<Factura>
{
    Task<Factura?> GetByOrdenAsync(int ordenId, CancellationToken ct = default);
}
