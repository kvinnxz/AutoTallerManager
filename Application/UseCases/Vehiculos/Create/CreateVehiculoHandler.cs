using Application.Abstractions;
using Application.Common;
using Domain.Entities.Taller;
using MediatR;

namespace Application.UseCases.Vehiculos.Create;

public sealed class CreateVehiculoHandler(IUnitOfWork uow) : IRequestHandler<CreateVehiculoCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateVehiculoCommand cmd, CancellationToken ct)
    {
        if (!await uow.Clientes.ExistsAsync(c => c.Id == cmd.ClienteId && c.IsActive, ct))
            return Result<int>.Failure($"Cliente {cmd.ClienteId} no encontrado.");
        if (await uow.Vehiculos.ExistsAsync(v => v.VinValue == cmd.VIN.ToUpper(), ct))
            return Result<int>.Failure($"Ya existe un vehículo con VIN '{cmd.VIN}'.");

        var v = new Vehiculo
        {
            ClienteId   = cmd.ClienteId, Marca = cmd.Marca, Modelo = cmd.Modelo,
            Anio        = cmd.Anio, VinValue  = cmd.VIN.ToUpperInvariant(),
            Kilometraje = cmd.Kilometraje
        };
        await uow.Vehiculos.AddAsync(v, ct);
        await uow.SaveChangesAsync(ct);
        return Result<int>.Success(v.Id);
    }
}
