using Domain.Common;
using Domain.Entities.Taller;
using Domain.ValueObjects;

namespace Domain.Entities.Auth;

public class Usuario : BaseEntity
{
    public string Nombre       { get; set; } = string.Empty;
    public string CorreoValue  { get; set; } = string.Empty;   // persisted
    public string PasswordHash { get; set; } = string.Empty;

    // Value Object (not mapped by EF directly as a column)
    public Email Correo
    {
        get => Email.Create(CorreoValue);
        set => CorreoValue = value.Value;
    }

    public ICollection<UsuarioRol>    UsuarioRoles     { get; set; } = new List<UsuarioRol>();
    public ICollection<RefreshToken>  RefreshTokens    { get; set; } = new List<RefreshToken>();
    public ICollection<OrdenServicio> OrdenesAsignadas { get; set; } = new List<OrdenServicio>();
}
