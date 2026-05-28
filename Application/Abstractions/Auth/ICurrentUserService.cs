namespace Application.Abstractions.Auth;

public interface ICurrentUserService
{
    int?    UserId { get; }
    string? Correo { get; }
    string? Nombre { get; }
    bool    IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
}
