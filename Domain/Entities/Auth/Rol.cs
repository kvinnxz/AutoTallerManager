using Domain.Common;

namespace Domain.Entities.Auth;

public class Rol : BaseEntity
{
    public string Nombre      { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
}
