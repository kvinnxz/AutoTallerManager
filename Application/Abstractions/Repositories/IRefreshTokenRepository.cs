using Domain.Entities.Auth;

namespace Application.Abstractions.Repositories;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);
    Task RevokeAllByUsuarioAsync(int usuarioId, CancellationToken ct = default);
}
