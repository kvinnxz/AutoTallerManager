using Application.Common;
using MediatR;

namespace Application.UseCases.Clientes.Delete;

public record DeleteClienteCommand(int Id) : IRequest<Result>;
