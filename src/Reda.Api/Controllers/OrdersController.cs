using MediatR;

using Microsoft.AspNetCore.Mvc;

using Reda.Application.Models;

namespace Reda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name = "SubmitOrder")]
    public async Task<ActionResult<SubmitOrderResponse>> Submit([FromBody] SubmitOrderRequest request)
    {
        var result = await _mediator.Send(request);

        return Ok(result);
    }

    [HttpGet("{orderId:int}", Name = "GetOrder")]
    public async Task<ActionResult<GetOrderResponse>> Get(int orderId)
    {
        var result = await _mediator.Send(new GetOrderRequest(orderId));

        return Ok(result);
    }
}