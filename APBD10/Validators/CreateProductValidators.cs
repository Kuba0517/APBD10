using APBD10.DTOs;
using FluentValidation;

namespace APBD10.Validators;

public class CreateProductValidators : AbstractValidator<CreateProductDTO>
{
    public CreateProductValidators()
    {
        RuleFor(e => e.ProductName).MaximumLength(100).NotNull().NotEmpty();
        RuleFor(e => e.ProductWeight).NotEmpty().NotNull();
        RuleFor(e => e.ProductWidth).NotEmpty().NotNull();
        RuleFor(e => e.ProductHeight).NotEmpty().NotNull();
        RuleFor(e => e.ProductDepth).NotEmpty().NotNull();
        RuleFor(e => e.ProductCategories).NotEmpty().NotNull();
    }
}