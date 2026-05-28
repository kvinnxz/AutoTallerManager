using Application.Abstractions;
using Application.Common;
using Application.UseCases.Clientes.GetAll;
using MediatR;

namespace Application.UseCases.Repuestos.GetAll;

public sealed class GetRepuestosHandler(IUnitOfWork uow)
    : IRequestHandler<GetRepuestosQuery, Result<PagedResult<RepuestoResult>>>
{
    public async Task<Result<PagedResult<RepuestoResult>>> Handle(GetRepuestosQuery qry, CancellationToken ct)
    {
        var (items, total) = await uow.Repuestos.GetPagedAsync(
            qry.Page, qry.PageSize,
            filter: r => r.IsActive &&
                (qry.Categoria   == null || r.Categoria.Contains(qry.Categoria)) &&
                (qry.Descripcion == null || r.Descripcion.Contains(qry.Descripcion)) &&
                (qry.BajoStock   == null || (qry.BajoStock.Value
                    ? r.CantidadStock <= r.StockMinimo
                    : r.CantidadStock > r.StockMinimo)),
            ct: ct);

        var results = items.Select(r => new RepuestoResult(
            r.Id, r.Codigo, r.Descripcion, r.Categoria,
            r.CantidadStock, r.StockMinimo, r.PrecioUnitario, r.BajoStock));

        return Result<PagedResult<RepuestoResult>>.Success(
            new PagedResult<RepuestoResult>(results, total, qry.Page, qry.PageSize));
    }
}
