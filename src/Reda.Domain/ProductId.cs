namespace Reda.Domain;

public record ProductId
{
    public long Value { get; }

    public ProductId(long value)
    {
        if (value == default)
            throw new ArgumentException($"{nameof(value)} cannot be default", nameof(value));

        Value = value;
    }
    
    public ProductId() => Value = 0;

    public static implicit operator long(ProductId productId) => productId.Value;
    public static implicit operator ProductId(long value) => new(value);
}