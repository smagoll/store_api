using Application.DTOs.Cart;
using FluentValidation;

public class RemoveFromCartDtoValidator : AbstractValidator<RemoveFromCartDto>
{
    public RemoveFromCartDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть больше 0");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId должен быть больше 0");
    }
}