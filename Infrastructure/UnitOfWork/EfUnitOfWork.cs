using Application.Abstractions;
using Application.Abstractions.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Auth;
using Infrastructure.Repositories.Taller;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork;

public class EfUnitOfWork(AppDbContext db) : IUnitOfWork
{
    private IDbContextTransaction? _tx;

    public IClienteRepository       Clientes        { get; } = new ClienteRepository(db);
    public IVehiculoRepository      Vehiculos       { get; } = new VehiculoRepository(db);
    public IOrdenServicioRepository OrdenesServicio { get; } = new OrdenServicioRepository(db);
    public IRepuestoRepository      Repuestos       { get; } = new RepuestoRepository(db);
    public IFacturaRepository       Facturas        { get; } = new FacturaRepository(db);
    public IUsuarioRepository       Usuarios        { get; } = new UsuarioRepository(db);
    public IRolRepository           Roles           { get; } = new RolRepository(db);
    public IRefreshTokenRepository  RefreshTokens   { get; } = new RefreshTokenRepository(db);

    public Task<int>  SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);

    public async Task BeginTransactionAsync(CancellationToken ct)
        => _tx = await db.Database.BeginTransactionAsync(ct);

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        if (_tx is not null) await _tx.CommitAsync(ct);
    }

    public async Task RollbackTransactionAsync(CancellationToken ct)
    {
        if (_tx is not null) await _tx.RollbackAsync(ct);
    }

    public void Dispose() { _tx?.Dispose(); db.Dispose(); }
}
