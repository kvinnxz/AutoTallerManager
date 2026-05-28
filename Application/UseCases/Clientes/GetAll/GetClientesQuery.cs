using Application.Common;
using MediatR;

namespace Application.UseCases.Clientes.GetAll;

public record GetClientesQuery(int Page, int PageSize, string? Nombre = null)
    : IRequest<Result<PagedResult<ClienteResult>>>;

public record ClienteResult(
    int Id, string Nombre, string Telefono,
    string Correo, DateTime CreatedAt, int TotalVehiculos);

public record PagedResult<T>(IEnumerable<T> Items, int Total, int Page, int PageSize);
