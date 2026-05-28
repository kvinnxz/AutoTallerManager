using Domain.Entities.Auth;

namespace Application.Abstractions.Repositories;

public interface IRolRepository : IGenericRepository<Rol>
{
    Task<Rol?> GetByNombreAsync(string nombre, CancellationToken ct = default);
}
