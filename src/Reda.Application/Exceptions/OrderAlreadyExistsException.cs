using Reda.Domain;

namespace Reda.Application.Exceptions;

public class OrderAlreadyExistsException : ApplicationException
{
    public OrderAlreadyExistsException(OrderId orderId)
        : base($"{nameof(OrderId)}=[{orderId}] already exists.")
    {
    }
}