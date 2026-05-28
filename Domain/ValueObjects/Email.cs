namespace Domain.ValueObjects;

public record Email
{
    public string Value { get; }
    private Email(string v) => Value = v;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
            throw new ArgumentException("Correo inválido.", nameof(value));
        return new Email(value.ToLowerInvariant().Trim());
    }

    public static implicit operator string(Email e) => e.Value;
    public override string ToString() => Value;
}
