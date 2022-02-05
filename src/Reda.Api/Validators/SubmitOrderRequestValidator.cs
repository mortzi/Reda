using FluentValidation;

using Reda.Application.Models;

namespace Reda.Api.Validators;

public class SubmitOrderRequestValidator : AbstractValidator<SubmitOrderRequest>
{
    public SubmitOrderRequestValidator()
    {
        RuleFor(r => r.OrderId).NotEmpty();
        RuleFor(r => r.Products).NotEmpty();
        RuleForEach(r => r.Products).NotEmpty().InjectValidator();
    }
}