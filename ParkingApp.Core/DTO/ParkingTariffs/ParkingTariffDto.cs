using ParkingApp.Core.Entities;

namespace ParkingApp.Core.DTOs.ParkingTariffs;

public record ParkingTariffDto(
    Guid Id,
    string Name,
    TimeSpan FreeParkingDuration,
    decimal HourlyRate,
    decimal DailyMaxRate,
    bool IsActive
)
{
    public ParkingTariff ToEntity()
    {
        return new ParkingTariff()
        {
            Id = Id,
            Name = Name,
            FreeParkingDuration = FreeParkingDuration,
            HourlyRate = HourlyRate,
            DailyMaxRate = DailyMaxRate,
            IsActive = IsActive,
        };
    }
}