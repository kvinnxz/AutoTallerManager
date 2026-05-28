using Application.Abstractions;
using Application.Common;
using Domain.Entities.Auth;
using MediatR;
 
namespace Application.UseCases.Auth.Register;
 
public sealed class RegisterHandler(IUnitOfWork uow) : IRequestHandler<RegisterCommand, Result<int>>
{
    public async Task<Result<int>> Handle(RegisterCommand cmd, CancellationToken ct)
    {
        if (await uow.Usuarios.ExistsAsync(u => u.CorreoValue == cmd.Correo.ToLower(), ct))
            return Result<int>.Failure($"Ya existe un usuario con el correo '{cmd.Correo}'.");

        var rol = await uow.Roles.FirstOrDefaultAsync(r => r.Nombre == cmd.Rol, ct);
        if (rol is null)
            return Result<int>.Failure($"El rol '{cmd.Rol}' no existe.");
 
        var usuario = new Usuario
        {
            Nombre       = cmd.Nombre,
            CorreoValue  = cmd.Correo.ToLowerInvariant().Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(cmd.Password),
            CreatedAt    = DateTime.UtcNow,
            IsActive     = true
        };
 
        await uow.Usuarios.AddAsync(usuario, ct);
        await uow.SaveChangesAsync(ct);
 
        await uow.UsuarioRoles.AsignarRolAsync(usuario.Id, rol.Id, ct);
        await uow.SaveChangesAsync(ct);
 
        return Result<int>.Success(usuario.Id);
    }
}