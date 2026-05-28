using Application.Abstractions.Repositories;
using Domain.Entities.Auth;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth;

public class RefreshTokenRepository(AppDbContext db)
    : GenericRepository<RefreshToken>(db), IRefreshTokenRepository
{
    public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct)
        => Set.Include(r => r.Usuario)
              .FirstOrDefaultAsync(r => r.Token == token && !r.IsRevoked, ct);

    public async Task RevokeAllByUsuarioAsync(int usuarioId, CancellationToken ct)
    {
        var tokens = await Set.Where(r => r.UsuarioId == usuarioId && !r.IsRevoked).ToListAsync(ct);
        foreach (var t in tokens) t.IsRevoked = true;
    }
}
