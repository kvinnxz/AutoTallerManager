using Application.Abstractions.Repositories;
using Domain.Entities.Taller;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Taller;

public class FacturaRepository(AppDbContext db)
    : GenericRepository<Factura>(db), IFacturaRepository
{
    public Task<Factura?> GetByOrdenAsync(int ordenId, CancellationToken ct)
        => Set.Include(f => f.OrdenServicio).ThenInclude(o => o.Detalles).ThenInclude(d => d.Repuesto)
              .Include(f => f.Cliente)
              .FirstOrDefaultAsync(f => f.OrdenServicioId == ordenId, ct);
}
