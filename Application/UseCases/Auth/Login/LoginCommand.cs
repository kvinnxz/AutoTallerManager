using Application.Common;
using MediatR;

namespace Application.UseCases.Auth.Login;

public record LoginCommand(string Correo, string Password)
    : IRequest<Result<LoginResult>>;

public record LoginResult(
    string AccessToken, string RefreshToken,
    string Nombre, string Correo,
    IEnumerable<string> Roles, DateTime Expiracion);
