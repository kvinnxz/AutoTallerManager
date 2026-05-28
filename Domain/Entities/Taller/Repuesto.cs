using Domain.Common;

namespace Domain.Entities.Taller;

public class Repuesto : BaseEntity
{
    public string  Codigo         { get; set; } = string.Empty;
    public string  Descripcion    { get; set; } = string.Empty;
    public string  Categoria      { get; set; } = string.Empty;
    public int     CantidadStock  { get; set; }
    public int     StockMinimo    { get; set; }
    public decimal PrecioUnitario { get; set; }

    public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();

    public bool BajoStock => CantidadStock <= StockMinimo;
}
