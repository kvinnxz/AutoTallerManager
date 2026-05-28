using Application.Common;
using Application.UseCases.Clientes.GetAll;
using MediatR;

namespace Application.UseCases.Clientes.GetById;

public record GetClienteByIdQuery(int Id) : IRequest<Result<ClienteResult>>;
