using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.Taller;

public class Cliente : BaseEntity
{
    public string Nombre        { get; set; } = string.Empty;
    public string TelefonoValue { get; set; } = string.Empty;
    public string CorreoValue   { get; set; } = string.Empty;

    public Telefono Telefono
    {
        get => Telefono.Create(TelefonoValue);
        set => TelefonoValue = value.Value;
    }

    public Email Correo
    {
        get => Email.Create(CorreoValue);
        set => CorreoValue = value.Value;
    }

    public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    public ICollection<Factura>  Facturas  { get; set; } = new List<Factura>();
}
