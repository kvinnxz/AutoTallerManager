using Application.Abstractions.Repositories;
using Domain.Entities.Auth;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth;

public class RolRepository(AppDbContext db)
    : GenericRepository<Rol>(db), IRolRepository
{
    public Task<Rol?> GetByNombreAsync(string nombre, CancellationToken ct)
        => Set.FirstOrDefaultAsync(r => r.Nombre == nombre, ct);

    public async Task AsignarRolAsync(int usuarioId, int rolId, CancellationToken ct = default)
    {
        var usuarioRol = new UsuarioRol { UsuarioId = usuarioId, RolId = rolId };
        await Db.UsuarioRoles.AddAsync(usuarioRol, ct);
    }
}
