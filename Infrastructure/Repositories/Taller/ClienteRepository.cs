using Application.Abstractions.Repositories;
using Domain.Entities.Taller;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Taller;

public class ClienteRepository(AppDbContext db)
    : GenericRepository<Cliente>(db), IClienteRepository
{
    public Task<Cliente?> GetByCorreoAsync(string correo, CancellationToken ct)
        => Set.FirstOrDefaultAsync(c => c.CorreoValue == correo.ToLower() && c.IsActive, ct);
}
