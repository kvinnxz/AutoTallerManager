using Application.Abstractions;
using Application.Common;
using Domain.Entities.Taller;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Ordenes.Cerrar;

public sealed class CerrarOrdenHandler(IUnitOfWork uow) : IRequestHandler<CerrarOrdenCommand, Result<FacturaCreadaResult>>
{
    private const decimal TasaIVA = 0.19m;

    public async Task<Result<FacturaCreadaResult>> Handle(CerrarOrdenCommand cmd, CancellationToken ct)
    {
        var orden = await uow.OrdenesServicio.FirstOrDefaultAsync(
            o => o.Id == cmd.OrdenId, ct, "Detalles", "Vehiculo");

        if (orden is null) return Result<FacturaCreadaResult>.Failure("Orden no encontrada.");
        if (orden.Estado == EstadoOrden.Cancelada)
            return Result<FacturaCreadaResult>.Failure("No se puede facturar una orden cancelada.");
        if (orden.Factura is not null)
            return Result<FacturaCreadaResult>.Failure("Esta orden ya tiene factura.");

        var subtotalRepuestos = orden.Detalles.Sum(d => d.Subtotal);
        var baseImponible     = orden.CostoManoObra + subtotalRepuestos;
        var impuesto          = Math.Round(baseImponible * TasaIVA, 2);

        var factura = new Factura
        {
            OrdenServicioId   = orden.Id,
            ClienteId         = orden.Vehiculo.ClienteId,
            NumeroFactura     = $"FAC-{DateTime.UtcNow:yyyyMMdd}-{orden.Id:D6}",
            FechaEmision      = DateTime.UtcNow,
            SubtotalRepuestos = subtotalRepuestos,
            CostoManoObra     = orden.CostoManoObra,
            Impuesto          = impuesto,
            Total             = baseImponible + impuesto,
        };

        orden.Estado          = EstadoOrden.Completada;
        orden.FechaEntregaReal = DateTime.UtcNow;
        orden.UpdatedAt       = DateTime.UtcNow;

        await uow.Facturas.AddAsync(factura, ct);
        uow.OrdenesServicio.Update(orden);
        await uow.SaveChangesAsync(ct);

        return Result<FacturaCreadaResult>.Success(
            new FacturaCreadaResult(factura.Id, factura.NumeroFactura, factura.Total));
    }
}
