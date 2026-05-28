using Application.Common;
using MediatR;

namespace Application.UseCases.Clientes.Create;

public record CreateClienteCommand(string Nombre, string Telefono, string Correo)
    : IRequest<Result<int>>;
