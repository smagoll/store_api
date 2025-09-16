using Application.DTOs.Cart;
using FluentValidation;

public class AddToCartDtoValidator : AbstractValidator<AddToCartDto>
{
    public AddToCartDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть больше 0");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId должен быть больше 0");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Количество должно быть больше 0");
    }
}