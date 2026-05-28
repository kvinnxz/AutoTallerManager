using Application.Common;
using MediatR;

namespace Application.UseCases.Ordenes.Cerrar;

public record CerrarOrdenCommand(int OrdenId) : IRequest<Result<FacturaCreadaResult>>;
public record FacturaCreadaResult(int FacturaId, string NumeroFactura, decimal Total);
