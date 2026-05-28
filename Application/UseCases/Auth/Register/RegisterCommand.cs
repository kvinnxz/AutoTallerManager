using Application.Common;
using MediatR;

namespace Application.UseCases.Auth.Register;

public record RegisterCommand(
    string Nombre, string Correo,
    string Password, string Rol)
    : IRequest<Result<int>>;
