using Domain.Common;

namespace Domain.Entities.Auth;

public class RefreshToken : BaseEntity
{
    public int      UsuarioId   { get; set; }
    public string   Token       { get; set; } = string.Empty;
    public DateTime ExpiresAt   { get; set; }
    public bool     IsRevoked   { get; set; }
    public string   CreatedByIp { get; set; } = string.Empty;

    public Usuario  Usuario     { get; set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsTokenActive => IsActive && !IsRevoked && !IsExpired;
}
