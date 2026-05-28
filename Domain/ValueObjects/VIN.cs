namespace Domain.ValueObjects;

public record VIN
{
    public string Value { get; }
    private VIN(string v) => Value = v;

    public static VIN Create(string value)
    {
        value = value.Trim().ToUpperInvariant();
        if (value.Length != 17)
            throw new ArgumentException("VIN debe tener 17 caracteres.", nameof(value));
        return new VIN(value);
    }

    public static implicit operator string(VIN v) => v.Value;
    public override string ToString() => Value;
}
