using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace Reda.Api.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IReadOnlyCollection<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators.ToArray();
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var validationFailures = new List<ValidationFailure>();

        foreach (var validator in _validators)
        {
            if (await validator.ValidateAsync(request, cancellationToken) is { IsValid: false } validationResult)
            {
                validationFailures.AddRange(validationResult.Errors);
            }
        }
        
        if (validationFailures.Any())
        {
            throw new ValidationException(validationFailures);
        }
        
        return await next();
    }
}