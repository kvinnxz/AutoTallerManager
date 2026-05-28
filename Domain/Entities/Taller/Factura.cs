using Domain.Common;

namespace Domain.Entities.Taller;

public class Factura : BaseEntity
{
    public int      OrdenServicioId   { get; set; }
    public int      ClienteId         { get; set; }
    public string   NumeroFactura     { get; set; } = string.Empty;
    public DateTime FechaEmision      { get; set; } = DateTime.UtcNow;
    public decimal  SubtotalRepuestos { get; set; }
    public decimal  CostoManoObra     { get; set; }
    public decimal  Impuesto          { get; set; }
    public decimal  Total             { get; set; }
    public bool     Pagada            { get; set; }

    public OrdenServicio OrdenServicio { get; set; } = null!;
    public Cliente       Cliente       { get; set; } = null!;
}
