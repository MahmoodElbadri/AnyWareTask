using AnyWareTask.Api.Dtos;
using FluentValidation;

namespace AnyWareTask.Api.Validators;

public class OrderRequestValidator : AbstractValidator<OrderRequestDto>
{
    public OrderRequestValidator()
    {
        RuleFor(tmp => tmp.CustomerName)
           .NotEmpty()
           .WithMessage("CustomerName is required")
           .MinimumLength(3)
           .WithMessage("CustomerName must be at least 3 characters long")
           .MaximumLength(50)
           .WithMessage("CustomerName must be at most 50 characters long");

        RuleFor(tmp => tmp.Product)
            .NotEmpty()
            .WithMessage("Product is required")
            .MinimumLength(3)
            .WithMessage("Product must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Product must be at most 50 characters long");

        RuleFor(tmp => tmp.Amount)
            .NotEmpty()
            .WithMessage("Quantity is required")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}
