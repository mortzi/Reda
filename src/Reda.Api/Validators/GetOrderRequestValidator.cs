using FluentValidation;

using Reda.Application.Models;

namespace Reda.Api.Validators;

public class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
{
    public GetOrderRequestValidator()
    {
        RuleFor(r => r.OrderId).NotEmpty();
    }
}