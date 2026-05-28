using System.Linq.Expressions;
using Domain.Common;

namespace Application.Abstractions.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?>             GetByIdAsync(int id, CancellationToken ct = default, params string[] includes);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default, params string[] includes);
    Task<(IEnumerable<T> Items, int Total)> GetPagedAsync(
        int page, int size,
        Expression<Func<T, bool>>? filter  = null,
        string[]? includes = null,
        CancellationToken ct = default);
    Task<T?>             FirstOrDefaultAsync(Expression<Func<T, bool>> pred, CancellationToken ct = default, params string[] includes);
    Task<bool>           ExistsAsync(Expression<Func<T, bool>> pred, CancellationToken ct = default);
    Task                 AddAsync(T entity, CancellationToken ct = default);
    void                 Update(T entity);
    void                 Remove(T entity);
}
