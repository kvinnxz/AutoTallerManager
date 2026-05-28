using Application.Common;
using Application.UseCases.Clientes.GetAll;
using MediatR;

namespace Application.UseCases.Facturas.GetAll;

public record GetFacturasQuery(
    int Page, int PageSize,
    int? ClienteId = null, int? OrdenId = null,
    DateTime? Desde = null, DateTime? Hasta = null)
    : IRequest<Result<PagedResult<FacturaResult>>>;

public record FacturaResult(
    int Id, string NumeroFactura, int ClienteId,
    string NombreCliente, int OrdenServicioId,
    DateTime FechaEmision, decimal SubtotalRepuestos,
    decimal CostoManoObra, decimal Impuesto,
    decimal Total, bool Pagada);
