using System.Security.Claims;
using Application.Abstractions.Auth;

namespace Api.Services.Auth;

public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => accessor.HttpContext?.User;

    public int?    UserId          => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
    public string? Correo          => User?.FindFirstValue(ClaimTypes.Email);
    public string? Nombre          => User?.FindFirstValue(ClaimTypes.Name);
    public bool    IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    public IEnumerable<string> Roles => User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];
}
