using Application.Common;
using MediatR;

namespace Application.UseCases.Clientes.Update;

public record UpdateClienteCommand(int Id, string? Nombre, string? Telefono, string? Correo)
    : IRequest<Result>;
