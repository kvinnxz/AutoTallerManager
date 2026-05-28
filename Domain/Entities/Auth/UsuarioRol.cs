using Domain.Common;

namespace Domain.Entities.Auth;

public class UsuarioRol : BaseEntity
{
    public int     UsuarioId { get; set; }
    public int     RolId     { get; set; }
    public Usuario Usuario   { get; set; } = null!;
    public Rol     Rol       { get; set; } = null!;
}
