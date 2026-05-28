using System.Linq.Expressions;
using Application.Abstractions.Repositories;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext db) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db = db;
    protected readonly DbSet<T> Set = db.Set<T>();

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default, params string[] includes)
    {
        IQueryable<T> q = Set;
        foreach (var i in includes) q = q.Include(i);
        return await q.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default, params string[] includes)
    {
        IQueryable<T> q = Set;
        foreach (var i in includes) q = q.Include(i);
        return await q.ToListAsync(ct);
    }

    public async Task<(IEnumerable<T> Items, int Total)> GetPagedAsync(
        int page, int size,
        Expression<Func<T, bool>>? filter = null,
        string[]? includes = null,
        CancellationToken ct = default)
    {
        IQueryable<T> q = Set;
        if (filter   is not null) q = q.Where(filter);
        if (includes is not null) foreach (var i in includes) q = q.Include(i);

        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * size).Take(size).ToListAsync(ct);
        return (items, total);
    }

    public async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> pred,
        CancellationToken ct = default,
        params string[] includes)
    {
        IQueryable<T> q = Set;
        foreach (var i in includes) q = q.Include(i);
        return await q.FirstOrDefaultAsync(pred, ct);
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> pred, CancellationToken ct = default)
        => Set.AnyAsync(pred, ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
        => await Set.AddAsync(entity, ct);

    public void Update(T entity) => Set.Update(entity);
    public void Remove(T entity) => Set.Remove(entity);
}
