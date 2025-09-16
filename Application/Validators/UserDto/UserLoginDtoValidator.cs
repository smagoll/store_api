using Application.DTOs.Auth;
using FluentValidation;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email).EmailRule();

        RuleFor(x => x.Password).NotEmpty().WithMessage("Пароль обязателен");
    }
}