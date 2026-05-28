using Domain.Entities.Auth;

namespace Application.Abstractions.Repositories;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario?> GetByCorreoAsync(string correo, CancellationToken ct = default);
    Task<bool>     TieneRolAsync(int usuarioId, string rol, CancellationToken ct = default);
    Task<IEnumerable<string>> GetRolesAsync(int usuarioId, CancellationToken ct = default);
}
