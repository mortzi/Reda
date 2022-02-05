namespace Reda.Domain;

public record ProductId
{
    public Guid Value { get; }

    public ProductId(Guid value)
    {
        if (value == default)
            throw new ArgumentException($"{nameof(value)} cannot be Guid.Empty", nameof(value));

        Value = value;
    }

    public static implicit operator Guid(ProductId productId) => productId.Value;
    public static implicit operator ProductId(Guid value) => new(value);
}