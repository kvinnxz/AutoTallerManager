using Application.Abstractions.Repositories;
using Domain.Entities.Auth;
using Infrastructure.Context;
 
namespace Infrastructure.Repositories.Auth;
 
public class UsuarioRolRepository(AppDbContext db)
    : GenericRepository<UsuarioRol>(db), IUsuarioRolRepository
{
    public async Task AsignarRolAsync(int usuarioId, int rolId, CancellationToken ct = default)
    {
        var existe = await ExistsAsync(ur => ur.UsuarioId == usuarioId && ur.RolId == rolId, ct);
        if (!existe)
        {
            var usuarioRol = new UsuarioRol
            {
                UsuarioId = usuarioId,
                RolId     = rolId,
                CreatedAt = DateTime.UtcNow,
                IsActive  = true
            };
            await AddAsync(usuarioRol, ct);
        }
    }
}