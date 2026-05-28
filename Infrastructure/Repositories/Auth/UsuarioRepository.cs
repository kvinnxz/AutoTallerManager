using Application.Abstractions.Repositories;
using Domain.Entities.Auth;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly AppDbContext _db;

    public UsuarioRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public Task<Usuario?> GetByCorreoAsync(string correo, CancellationToken ct)
        => Set.Include(u => u.UsuarioRoles).ThenInclude(ur => ur.Rol)
              .FirstOrDefaultAsync(u => u.CorreoValue == correo.ToLower() && u.IsActive, ct);

    public Task<bool> TieneRolAsync(int usuarioId, string rol, CancellationToken ct)
        => _db.UsuarioRoles.AnyAsync(
            ur => ur.UsuarioId == usuarioId && ur.Rol.Nombre == rol && ur.IsActive, ct);

    public async Task<IEnumerable<string>> GetRolesAsync(int usuarioId, CancellationToken ct)
        => await _db.UsuarioRoles
            .Where(ur => ur.UsuarioId == usuarioId && ur.IsActive)
            .Select(ur => ur.Rol.Nombre)
            .ToListAsync(ct);
}
