namespace Domain.ValueObjects;

public record Dinero
{
    public decimal Monto  { get; }
    public string  Moneda { get; }

    private Dinero(decimal m, string c) { Monto = m; Moneda = c; }

    public static Dinero Create(decimal monto, string moneda = "COP")
    {
        if (monto < 0) throw new ArgumentException("Monto no puede ser negativo.");
        return new Dinero(Math.Round(monto, 2), moneda.ToUpperInvariant());
    }

    public static implicit operator decimal(Dinero d) => d.Monto;
    public override string ToString() => $"{Moneda} {Monto:N2}";
}
