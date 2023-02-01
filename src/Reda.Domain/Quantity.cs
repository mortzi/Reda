namespace Reda.Domain;

/// <summary>
/// Represents a wrapper value object for Quantity
/// </summary>
public record Quantity
{
    public int Value { get; }

    public Quantity(int value)
    {
        Value = value < 0 ? throw new ArgumentException("Quantity cannot be less than 0", nameof(value)) : value;
    }

    public static implicit operator int(Quantity quantity) => quantity.Value;
    public static implicit operator Quantity(int value) => new(value);
}