using Domain.Common;

namespace Domain.Entities.Taller;

public class DetalleOrden : BaseEntity
{
    public int     OrdenServicioId { get; set; }
    public int     RepuestoId      { get; set; }
    public int     Cantidad        { get; set; }
    public decimal PrecioUnitario  { get; set; }

    public OrdenServicio OrdenServicio { get; set; } = null!;
    public Repuesto      Repuesto      { get; set; } = null!;

    public decimal Subtotal => Cantidad * PrecioUnitario;
}
