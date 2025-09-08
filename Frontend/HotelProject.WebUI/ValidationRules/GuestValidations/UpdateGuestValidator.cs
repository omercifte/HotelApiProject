using FluentValidation;
using HotelProject.WebUI.Dtos.GuestDto;

namespace HotelProject.WebUI.ValidationRules.GuestValidations
{
    public class UpdateGuestValidator:AbstractValidator<UpdateGuestDto>
    {
        public UpdateGuestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("İsim alanı boş olamaz")
               .MinimumLength(3).WithMessage("İsim min 3 karakter olmalı");
            RuleFor(x => x.Surname)
               .NotEmpty().WithMessage("Soyisim alanı boş olamaz")
               .MinimumLength(2).WithMessage("İsim min 2 karakter olmalı");
            RuleFor(x => x.City)
               .NotEmpty().WithMessage("Şehir alanı boş olamaz")
               .MinimumLength(3).WithMessage("Şehir min 3 karakter olmalı");
        }
    }
}
