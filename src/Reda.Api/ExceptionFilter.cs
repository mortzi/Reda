using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Reda.Application.Exceptions;

namespace Reda.Api;

public class ExceptionFilter : IExceptionFilter, IOrderedFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public int Order => int.MaxValue - 10;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            OrderNotFoundException => new NotFoundResult(),
            OrderAlreadyExistsException => new BadRequestResult(),
            InvalidProductTypeException => new BadRequestResult(),
            _ => context.Result
        };
        
        _logger.LogError(context.Exception, "Error when executing Action");
    }
}