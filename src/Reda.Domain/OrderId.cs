namespace Reda.Domain;

/// <summary>
/// Represents a wrapper value object for Order Id
/// </summary>
public record OrderId
{
    public Guid Value { get; }

    public OrderId(Guid value)
    {
        if (value == default)
            throw new ArgumentException($"{nameof(value)} cannot be Guid.Empty", nameof(value));

        Value = value;
    }

    public static implicit operator Guid(OrderId orderId) => orderId.Value;
    public static implicit operator OrderId(Guid value) => new(value);
}