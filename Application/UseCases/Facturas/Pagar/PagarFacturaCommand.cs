using Application.Common;
using MediatR;
namespace Application.UseCases.Facturas.Pagar;
public record PagarFacturaCommand(int FacturaId) : IRequest<Result>;
