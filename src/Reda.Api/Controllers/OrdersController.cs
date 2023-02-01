using FluentValidation;
using FluentValidation.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Reda.Application.Models;

namespace Reda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<SubmitOrderRequest> _submitOrderValidator;
    private readonly IValidator<GetOrderRequest> _getOrderValidator;

    public OrdersController(
        IMediator mediator,
        IValidator<SubmitOrderRequest> submitOrderValidator,
        IValidator<GetOrderRequest> getOrderValidator)
    {
        _mediator = mediator;
        _submitOrderValidator = submitOrderValidator;
        _getOrderValidator = getOrderValidator;
    }

    [HttpPost(Name = "SubmitOrder")]
    public async Task<ActionResult<SubmitOrderResponse>> Submit([FromBody] SubmitOrderRequest request)
    {
        if (await _submitOrderValidator.ValidateAsync(request) is { IsValid: false } validationResult)
        {
            validationResult.AddToModelState(ModelState);
            return ValidationProblem(ModelState);
        }
        
        var result = await _mediator.Send(request);

        return Ok(result);
    }

    [HttpGet(Name = "GetOrder")]
    public async Task<ActionResult<GetOrderResponse>> Get([FromQuery] GetOrderRequest request)
    {
        if (await _getOrderValidator.ValidateAsync(request) is { IsValid: false } validationResult)
        {
            validationResult.AddToModelState(ModelState);
            return ValidationProblem(ModelState);
        }
        
        var result = await _mediator.Send(request);

        return Ok(result);
    }
}