namespace Reda.Domain;

/// <summary>
/// Represents a wrapper value object for Length in millimeters
/// </summary>
public record Width
{
    /// <summary>
    /// Width value in millimeters
    /// </summary>
    public double Value { get; }

    public Width(double value)
    {
        Value = value < 0 ? throw new ArgumentException(nameof(value)) : value;
    }

    public override string ToString()
    {
        return $"{Value} mm";
    }

    public static implicit operator double(Width width) => width.Value;
    public static implicit operator Width(double value) => new(value);
}