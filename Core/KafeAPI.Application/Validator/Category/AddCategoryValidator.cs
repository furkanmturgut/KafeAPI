using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;

namespace KafeAPI.Application.Validator.Category
{
    public class AddCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .Length(3, 30).WithMessage("Kategori adı en az 3 en fazla 30 karakter olabilir.");
        }
    }
}
