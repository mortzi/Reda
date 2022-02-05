namespace Reda.Application.Exceptions;

public class InvalidProductTypeException : Exception
{
    public InvalidProductTypeException(string productName)
        : base($"{nameof(productName)}=[{productName}] is invalid.")
    {
    }
}