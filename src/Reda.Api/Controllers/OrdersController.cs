using MediatR;

using Microsoft.AspNetCore.Mvc;

using Reda.Application.Models;

namespace Reda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "SubmitOrder")]
    public async Task<ActionResult<SubmitOrderResponse>> Submit(SubmitOrderRequest request)
    {
        var result = await _mediator.Send(request);

        return Ok(result);
    }

    [HttpGet(Name = "GetOrder")]
    public async Task<ActionResult<GetOrderResponse>> Get([FromQuery] GetOrderRequest request)
    {
        var result = await _mediator.Send(request);

        return Ok(result);
    }
}