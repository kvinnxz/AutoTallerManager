using Domain.Common;
using Domain.Entities.Auth;
using Domain.Enums;

namespace Domain.Entities.Taller;

public class OrdenServicio : BaseEntity
{
    public int           VehiculoId            { get; set; }
    public int           MecanicoId            { get; set; }
    public TipoServicio  TipoServicio          { get; set; }
    public EstadoOrden   Estado                { get; set; } = EstadoOrden.Pendiente;
    public DateTime      FechaIngreso          { get; set; } = DateTime.UtcNow;
    public DateTime?     FechaEstimadaEntrega  { get; set; }
    public DateTime?     FechaEntregaReal      { get; set; }
    public string        Descripcion           { get; set; } = string.Empty;
    public string        TrabajoRealizado      { get; set; } = string.Empty;
    public decimal       CostoManoObra         { get; set; }

    public Vehiculo                  Vehiculo  { get; set; } = null!;
    public Usuario                   Mecanico  { get; set; } = null!;
    public ICollection<DetalleOrden> Detalles  { get; set; } = new List<DetalleOrden>();
    public Factura?                  Factura   { get; set; }
}
