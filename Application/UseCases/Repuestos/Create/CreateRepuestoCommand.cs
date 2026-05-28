using Application.Common;
using MediatR;
namespace Application.UseCases.Repuestos.Create;
public record CreateRepuestoCommand(
    string Codigo, string Descripcion, string Categoria,
    int CantidadStock, int StockMinimo, decimal PrecioUnitario)
    : IRequest<Result<int>>;
