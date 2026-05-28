using Application.Abstractions.Repositories;
using Domain.Entities.Taller;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Taller;

public class VehiculoRepository(AppDbContext db)
    : GenericRepository<Vehiculo>(db), IVehiculoRepository
{
    public Task<Vehiculo?> GetByVinAsync(string vin, CancellationToken ct)
        => Set.FirstOrDefaultAsync(v => v.VinValue == vin.ToUpper() && v.IsActive, ct);
}
