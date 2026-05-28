using Application.Abstractions;
using Application.Common;
using Domain.Entities.Taller;
using MediatR;

namespace Application.UseCases.Repuestos.Create;

public sealed class CreateRepuestoHandler(IUnitOfWork uow) : IRequestHandler<CreateRepuestoCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateRepuestoCommand cmd, CancellationToken ct)
    {
        if (await uow.Repuestos.ExistsAsync(r => r.Codigo == cmd.Codigo.ToUpper(), ct))
            return Result<int>.Failure($"Ya existe un repuesto con código '{cmd.Codigo}'.");

        var repuesto = new Repuesto
        {
            Codigo         = cmd.Codigo.ToUpperInvariant(),
            Descripcion    = cmd.Descripcion,
            Categoria      = cmd.Categoria,
            CantidadStock  = cmd.CantidadStock,
            StockMinimo    = cmd.StockMinimo,
            PrecioUnitario = cmd.PrecioUnitario
        };

        await uow.Repuestos.AddAsync(repuesto, ct);
        await uow.SaveChangesAsync(ct);
        return Result<int>.Success(repuesto.Id);
    }
}
