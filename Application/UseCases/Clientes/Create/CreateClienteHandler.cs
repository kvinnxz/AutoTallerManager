using Application.Abstractions;
using Application.Common;
using Domain.Entities.Taller;
using MediatR;

namespace Application.UseCases.Clientes.Create;

public sealed class CreateClienteHandler(IUnitOfWork uow)
    : IRequestHandler<CreateClienteCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateClienteCommand cmd, CancellationToken ct)
    {
        if (await uow.Clientes.ExistsAsync(c => c.CorreoValue == cmd.Correo.ToLower(), ct))
            return Result<int>.Failure($"Ya existe un cliente con el correo '{cmd.Correo}'.");

        var cliente = new Cliente
        {
            Nombre       = cmd.Nombre,
            TelefonoValue = cmd.Telefono,
            CorreoValue  = cmd.Correo.ToLowerInvariant()
        };

        await uow.Clientes.AddAsync(cliente, ct);
        await uow.SaveChangesAsync(ct);
        return Result<int>.Success(cliente.Id);
    }
}
