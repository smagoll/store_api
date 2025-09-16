using Application.DTOs;
using FluentValidation;

public class DeleteProductDtoValidator : AbstractValidator<DeleteProductDto>
{
    public DeleteProductDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id продукта должен быть больше 0");
    }
}