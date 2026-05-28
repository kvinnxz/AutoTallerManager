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
            filter:   c => c.IsActive && (qry.Nombre == null || c.Nombre.Contains(qry.Nombre)),
            includes: ["Vehiculos"],
            ct:       ct);

        var results = items.Select(c => new ClienteResult(
            c.Id, c.Nombre, c.TelefonoValue, c.CorreoValue,
            c.CreatedAt, c.Vehiculos.Count));

        return Result<PagedResult<ClienteResult>>.Success(
            new PagedResult<ClienteResult>(results, total, qry.Page, qry.PageSize));
    }
}
