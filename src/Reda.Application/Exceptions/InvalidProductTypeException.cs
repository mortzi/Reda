namespace Reda.Application.Exceptions;

public class InvalidProductTypeException : ApplicationException
{
    public InvalidProductTypeException(string productName)
        : base($"{nameof(productName)}=[{productName}] is invalid.")
    {
    }
}