using FluentValidation;
using ParkingApp.Core.DTOs.ParkingTariffs;

namespace ParkingApp.Core.Validators;

public class ParkingTariffValidator : AbstractValidator<CreateTariffDto>
{
    public ParkingTariffValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nazwa taryfy jest wymagana.")
            .MaximumLength(50).WithMessage("Nazwa nie może przekraczać 50 znaków.");

        RuleFor(x => x.FreeMinutes)
            .GreaterThanOrEqualTo(0).WithMessage("Liczba bezpłatnych minut nie może być ujemna.");

        RuleFor(x => x.HourlyRate)
            .GreaterThan(0).WithMessage("Stawka godzinowa musi być większa od zera.")
            .PrecisionScale(8, 2, false).WithMessage("Stawka godzinowa może mieć maksymalnie 2 miejsca po przecinku.");

        RuleFor(x => x.DailyMaxRate)
            .GreaterThan(0).WithMessage("Maksymalna stawka dobowa musi być większa od zera.")
            .PrecisionScale(8, 2, false).WithMessage("Maksymalna stawka dobowa może mieć maksymalnie 2 miejsca po przecinku.")
            .GreaterThanOrEqualTo(x => x.HourlyRate)
            .WithMessage("Maksymalna stawka dobowa nie może być niższa niż stawka godzinowa.");
    }
}
