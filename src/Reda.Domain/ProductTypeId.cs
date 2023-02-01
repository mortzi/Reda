namespace Reda.Domain;

public record ProductTypeId
{
    public long Value { get; }

    public ProductTypeId(long value)
    {
        if (value == default)
            throw new ArgumentException($"{nameof(value)} cannot be default", nameof(value));

        Value = value;
    }

    public static implicit operator long(ProductTypeId productTypeId) => productTypeId.Value;
    public static implicit operator ProductTypeId(long value) => new(value);
}