using Application.Abstractions.Repositories;
using Domain.Entities.Taller;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Taller;

public class RepuestoRepository(AppDbContext db)
    : GenericRepository<Repuesto>(db), IRepuestoRepository
{
    public Task<Repuesto?> GetByCodigoAsync(string codigo, CancellationToken ct)
        => Set.FirstOrDefaultAsync(r => r.Codigo == codigo.ToUpper() && r.IsActive, ct);
}
