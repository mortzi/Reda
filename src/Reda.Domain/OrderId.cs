namespace Reda.Domain;

/// <summary>
/// Represents a wrapper value object for Order Id
/// </summary>
public record OrderId
{
    public long Value { get; }

    public OrderId(long value)
    {
        if (value < 1)
            throw new ArgumentException($"{nameof(value)} must be non-negative", nameof(value));

        Value = value;
    }

    public static implicit operator long(OrderId orderId) => orderId.Value;
    public static implicit operator OrderId(long value) => new(value);
}