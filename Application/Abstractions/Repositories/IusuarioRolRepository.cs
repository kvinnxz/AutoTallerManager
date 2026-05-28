using Domain.Entities.Auth;
 
namespace Application.Abstractions.Repositories;
 
public interface IUsuarioRolRepository : IGenericRepository<UsuarioRol>
{
    Task AsignarRolAsync(int usuarioId, int rolId, CancellationToken ct = default);
}
 