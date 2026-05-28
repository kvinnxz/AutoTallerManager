using Domain.Entities.Auth;

namespace Application.Abstractions.Auth;

public interface ITokenService
{
    string GenerateAccessToken(Usuario usuario, IEnumerable<string> roles);
    string GenerateRefreshToken();
}
