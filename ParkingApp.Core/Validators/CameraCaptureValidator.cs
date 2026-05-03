using FluentValidation;
using ParkingApp.Core.DTOs.CameraCaptures;

namespace ParkingApp.Core.Validators;

public class CameraCaptureValidator : AbstractValidator<CameraCaptureDto>
{
    public CameraCaptureValidator()
    {
        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage("Numer rejestracyjny jest wymagany.")
            .MaximumLength(15).WithMessage("Numer rejestracyjny nie może przekraczać 15 znaków.")
            .Matches(@"^[A-Z0-9\s\-]+$").WithMessage("Numer rejestracyjny zawiera niedozwolone znaki.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Marka pojazdu jest wymagana.")
            .MaximumLength(50).WithMessage("Marka pojazdu nie może przekraczać 50 znaków.");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Kolor pojazdu jest wymagany.")
            .MaximumLength(30).WithMessage("Kolor pojazdu nie może przekraczać 30 znaków.")
            .Matches(@"^[\p{L}\s\-]+$").WithMessage("Kolor pojazdu zawiera niedozwolone znaki.");

        RuleFor(x => x.GateName)
            .NotEmpty().WithMessage("Nazwa bramki jest wymagana.")
            .MaximumLength(20).WithMessage("Nazwa bramki nie może przekraczać 20 znaków.");

        When(x => x.ImagePath is not null, () =>
        {
            RuleFor(x => x.ImagePath)
                .Matches(@"^.+\.(jpg|jpeg|png|bmp)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                .WithMessage("Ścieżka do zdjęcia musi wskazywać na plik jpg, jpeg, png lub bmp.");
        });
    }
}
