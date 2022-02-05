using FluentValidation;

using Reda.Application.Models;

namespace Reda.Api.Validators;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(p => p.Quantity).NotEmpty().Must(q => q >= 0);
        RuleFor(p => p.ProductType).NotEmpty();
    }
}