using Application.Common;
using MediatR;
namespace Application.UseCases.Repuestos.AjustarStock;
public record AjustarStockCommand(int RepuestoId, int Cantidad, string Motivo) : IRequest<Result>;
