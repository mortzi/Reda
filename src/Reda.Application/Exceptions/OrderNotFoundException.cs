using Reda.Domain;

namespace Reda.Application.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(OrderId orderId)
        : base($"{nameof(orderId)}=[{orderId}] does not exist.")
    {
    }
}