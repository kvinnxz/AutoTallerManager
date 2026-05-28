namespace Domain.ValueObjects;

public record Telefono
{
    public string Value { get; }
    private Telefono(string v) => Value = v;

    public static Telefono Create(string value)
    {
        var d = new string(value.Where(char.IsDigit).ToArray());
        if (d.Length < 7 || d.Length > 15)
            throw new ArgumentException("Teléfono debe tener entre 7 y 15 dígitos.", nameof(value));
        return new Telefono(value.Trim());
    }

    public static implicit operator string(Telefono t) => t.Value;
    public override string ToString() => Value;
}
