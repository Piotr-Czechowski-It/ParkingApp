using FluentValidation;
using ParkingApp.Core.DTOs.ParkingGates;
using ParkingApp.Core.Enums;

namespace ParkingApp.Core.Validators;

public class ParkingGateValidator : AbstractValidator<CreateGateDto>
{
    public ParkingGateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nazwa bramki jest wymagana.")
            .MaximumLength(20).WithMessage("Nazwa nie może przekraczać 20 znaków.")
            .Matches(@"^[\p{L}\s\-]+$").WithMessage("Nazwa zawiera niedozwolone znaki.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Lokalizacja bramki jest wymagana.")
            .MaximumLength(50).WithMessage("Lokalizacja nie może przekraczać 50 znaków.")
            .Matches(@"^[\p{L}\s\-]+$").WithMessage("Lokalizacja zawiera niedozwolone znaki.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Typ bramki jest wymagany.")
            .Must(t => Enum.TryParse<GateType>(t, ignoreCase: true, out _))
            .WithMessage($"Typ bramki musi być jedną z wartości: {string.Join(", ", Enum.GetNames<GateType>())}.");
    }
}
