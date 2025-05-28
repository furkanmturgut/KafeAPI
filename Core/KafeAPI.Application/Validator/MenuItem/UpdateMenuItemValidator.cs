using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Validator.MenuItem
{
    public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemDto>
    {
        public UpdateMenuItemValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Kategori Id boş olamaz.");
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Menü öğesi adı boş olamaz.")
                .Length(3, 50).WithMessage("Menü öğesi adı en az 3 en fazla 50 karakter olabilir.");
            RuleFor(m => m.Description)
                .NotEmpty().WithMessage("Menü öğesi açıklaması boş olamaz.")
                .Length(10, 200).WithMessage("Menü öğesi açıklaması en az 10 en fazla 200 karakter olabilir.");
            RuleFor(m => m.Price)
                .GreaterThan(0).WithMessage("Menü öğesi fiyatı sıfırdan büyük olmalıdır.");

        }
    }
}
