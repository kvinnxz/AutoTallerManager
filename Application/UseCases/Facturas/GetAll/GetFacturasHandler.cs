using Application.Abstractions;
using Application.Common;
using Application.UseCases.Clientes.GetAll;
using MediatR;

namespace Application.UseCases.Facturas.GetAll;

public sealed class GetFacturasHandler(IUnitOfWork uow)
    : IRequestHandler<GetFacturasQuery, Result<PagedResult<FacturaResult>>>
{
    public async Task<Result<PagedResult<FacturaResult>>> Handle(GetFacturasQuery qry, CancellationToken ct)
    {
        var (items, total) = await uow.Facturas.GetPagedAsync(
            qry.Page, qry.PageSize,
            filter: f =>
                (qry.ClienteId == null || f.ClienteId       == qry.ClienteId) &&
                (qry.OrdenId   == null || f.OrdenServicioId == qry.OrdenId) &&
                (qry.Desde     == null || f.FechaEmision    >= qry.Desde) &&
                (qry.Hasta     == null || f.FechaEmision    <= qry.Hasta),
            includes: ["Cliente"],
            ct: ct);

        var results = items.Select(f => new FacturaResult(
            f.Id, f.NumeroFactura, f.ClienteId,
            f.Cliente?.Nombre ?? string.Empty,
            f.OrdenServicioId, f.FechaEmision,
            f.SubtotalRepuestos, f.CostoManoObra,
            f.Impuesto, f.Total, f.Pagada));

        return Result<PagedResult<FacturaResult>>.Success(
            new PagedResult<FacturaResult>(results, total, qry.Page, qry.PageSize));
    }
}
