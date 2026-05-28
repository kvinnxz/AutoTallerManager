using Application.Abstractions.Repositories;

namespace Application.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IClienteRepository       Clientes        { get; }
    IVehiculoRepository      Vehiculos       { get; }
    IOrdenServicioRepository OrdenesServicio { get; }
    IRepuestoRepository      Repuestos       { get; }
    IFacturaRepository       Facturas        { get; }
    IUsuarioRepository       Usuarios        { get; }
    IRolRepository           Roles           { get; }
    IRefreshTokenRepository  RefreshTokens   { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitTransactionAsync(CancellationToken ct = default);
    Task RollbackTransactionAsync(CancellationToken ct = default);
}
