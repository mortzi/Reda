using Reda.Domain;

namespace Reda.Application.Exceptions;

public class OrderNotFoundException : ApplicationException
{
    public OrderNotFoundException(OrderId orderId)
        : base($"{nameof(orderId)}=[{orderId}] does not exist.")
    {
    }
}