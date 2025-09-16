using Application.DTOs;
using FluentValidation;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id продукта должен быть больше 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название продукта обязательно")
            .MaximumLength(100).WithMessage("Название продукта не должно превышать 100 символов");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Цена продукта должна быть больше 0");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId должен быть больше 0");
    }
}