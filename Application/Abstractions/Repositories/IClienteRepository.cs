using Domain.Entities.Taller;

namespace Application.Abstractions.Repositories;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<Cliente?> GetByCorreoAsync(string correo, CancellationToken ct = default);
}
