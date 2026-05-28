using Application.Abstractions;
using Application.Common;
using MediatR;

namespace Application.UseCases.Clientes.Update;

public sealed class UpdateClienteHandler(IUnitOfWork uow)
    : IRequestHandler<UpdateClienteCommand, Result>
{
    public async Task<Result> Handle(UpdateClienteCommand cmd, CancellationToken ct)
    {
        var cliente = await uow.Clientes.GetByIdAsync(cmd.Id, ct);
        if (cliente is null || !cliente.IsActive) return Result.Failure("Cliente no encontrado.");

        if (cmd.Nombre   is not null) cliente.Nombre        = cmd.Nombre;
        if (cmd.Telefono is not null) cliente.TelefonoValue = cmd.Telefono;
        if (cmd.Correo   is not null) cliente.CorreoValue   = cmd.Correo.ToLowerInvariant();

        cliente.UpdatedAt = DateTime.UtcNow;
        uow.Clientes.Update(cliente);
        await uow.SaveChangesAsync(ct);
        return Result.Success();
    }
}
