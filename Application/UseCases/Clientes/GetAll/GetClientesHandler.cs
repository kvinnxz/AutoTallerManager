using Application.Abstractions;
using Application.Common;
using MediatR;

namespace Application.UseCases.Clientes.GetAll;

public sealed class GetClientesHandler(IUnitOfWork uow)
    : IRequestHandler<GetClientesQuery, Result<PagedResult<ClienteResult>>>
{
    public async Task<Result<PagedResult<ClienteResult>>> Handle(GetClientesQuery qry, CancellationToken ct)
    {
        var (items, total) = await uow.Clientes.GetPagedAsync(
            qry.Page, qry.PageSize,
            filter: c => c.IsActive && (qry.Nombre == null || c.Nombre.Contains(qry.Nombre)),
            ct: ct);

        // Cargar vehículos por separado para evitar el bug con include + paginación
        var results = new List<ClienteResult>();
        foreach (var c in items)
        {
            var vehiculos = await uow.Vehiculos.GetPagedAsync(
                1, int.MaxValue,
                filter: v => v.ClienteId == c.Id && v.IsActive,
                ct: ct);

            results.Add(new ClienteResult(
                c.Id, c.Nombre, c.TelefonoValue, c.CorreoValue,
                c.CreatedAt, vehiculos.Total));
        }

        return Result<PagedResult<ClienteResult>>.Success(
            new PagedResult<ClienteResult>(results, total, qry.Page, qry.PageSize));
    }
}