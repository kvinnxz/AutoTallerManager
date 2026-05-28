using Application.Abstractions;
using Application.Abstractions.Auth;
using Application.Common;
using Domain.Entities.Auth;
using MediatR;

namespace Application.UseCases.Auth.Login;

public sealed class LoginHandler(IUnitOfWork uow, ITokenService tokenSvc)
    : IRequestHandler<LoginCommand, Result<LoginResult>>
{
    public async Task<Result<LoginResult>> Handle(LoginCommand cmd, CancellationToken ct)
    {
        var usuario = await uow.Usuarios.GetByCorreoAsync(cmd.Correo, ct);
        if (usuario is null || !BCrypt.Net.BCrypt.Verify(cmd.Password, usuario.PasswordHash))
            return Result<LoginResult>.Failure("Credenciales inválidas.");

        if (!usuario.IsActive)
            return Result<LoginResult>.Failure("Usuario inactivo.");

        var roles       = await uow.Usuarios.GetRolesAsync(usuario.Id, ct);
        var accessToken = tokenSvc.GenerateAccessToken(usuario, roles);
        var rawRefresh  = tokenSvc.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            UsuarioId  = usuario.Id,
            Token      = BCrypt.Net.BCrypt.HashPassword(rawRefresh),
            ExpiresAt  = DateTime.UtcNow.AddDays(7),
            CreatedAt  = DateTime.UtcNow
        };

        await uow.RefreshTokens.AddAsync(refreshToken, ct);
        await uow.SaveChangesAsync(ct);

        return Result<LoginResult>.Success(new LoginResult(
            accessToken, rawRefresh,
            usuario.Nombre, usuario.CorreoValue,
            roles, DateTime.UtcNow.AddHours(8)));
    }
}
