using Reda.Domain;

namespace Reda.Application.Exceptions;

public class OrderAlreadyExistsException : Exception
{
    public OrderAlreadyExistsException(OrderId orderId)
        : base($"{nameof(OrderId)}=[{orderId}] is invalid.")
    {
    }
}