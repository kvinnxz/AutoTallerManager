using Application.Common;
using Application.UseCases.Clientes.GetAll;
using MediatR;

namespace Application.UseCases.Repuestos.GetAll;

public record GetRepuestosQuery(
    int Page, int PageSize,
    string? Categoria = null,
    string? Descripcion = null,
    bool? BajoStock = null)
    : IRequest<Result<PagedResult<RepuestoResult>>>;

public record RepuestoResult(
    int Id, string Codigo, string Descripcion,
    string Categoria, int CantidadStock,
    int StockMinimo, decimal PrecioUnitario, bool BajoStock);
