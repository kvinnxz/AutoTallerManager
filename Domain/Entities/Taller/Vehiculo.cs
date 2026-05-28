using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.Taller;

public class Vehiculo : BaseEntity
{
    public int    ClienteId    { get; set; }
    public string Marca        { get; set; } = string.Empty;
    public string Modelo       { get; set; } = string.Empty;
    public int    Anio         { get; set; }
    public string VinValue     { get; set; } = string.Empty;
    public int    Kilometraje  { get; set; }

    public VIN Vin
    {
        get => VIN.Create(VinValue);
        set => VinValue = value.Value;
    }

    public Cliente                    Cliente         { get; set; } = null!;
    public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
}
