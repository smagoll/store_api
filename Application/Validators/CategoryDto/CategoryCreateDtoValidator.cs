using Application.DTOs;
using FluentValidation;

namespace Application.Validators.CategoryDto;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Название категории обязательно")
            .MaximumLength(100).WithMessage("Название категории не должно превышать 100 символов");
    }
}