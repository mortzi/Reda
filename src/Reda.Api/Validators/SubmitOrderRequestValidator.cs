using FluentValidation;

using Reda.Application.Models;

namespace Reda.Api.Validators;

public class SubmitOrderRequestValidator : AbstractValidator<SubmitOrderRequest>
{
    public SubmitOrderRequestValidator(IValidator<ProductRequest> productValidator)
    {
        RuleFor(r => r.OrderId).NotEmpty().Must(id => id > 0);
        RuleFor(r => r.Products).NotEmpty();
        RuleForEach(r => r.Products).NotEmpty().SetValidator(productValidator);
    }
}