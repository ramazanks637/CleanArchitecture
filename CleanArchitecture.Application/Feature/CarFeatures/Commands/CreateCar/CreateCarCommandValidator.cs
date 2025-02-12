using FluentValidation;

namespace CleanArchitecture.Application.Feature.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Araç Adı boş olamaz !")
                .NotNull()
                .MaximumLength(3).WithMessage("Araç adı minimum 3 karakter olmalı !");

            RuleFor(p => p.Model)
                .NotEmpty().WithMessage("Araç Modeli boş olamaz !")
                .NotNull()
                .MaximumLength(3).WithMessage("Araç modeli minimum 3 karakter olmalı !");

            RuleFor(p => p.EnginePower)
                .NotEmpty().WithMessage("Motor Gücü boş olamaz !")
                .NotNull()
                .GreaterThan(0).WithMessage("Motor gücü 0'dan büyük olmalı !");

        }
    }
}
