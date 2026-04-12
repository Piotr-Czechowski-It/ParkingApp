using ParkingApp.Core.Entities;

namespace ParkingApp.Core.DTOs.ParkingTariffs;

public record CreateTariffDto(
    string Name,
    int FreeMinutes,
    decimal HourlyRate,
    decimal DailyMaxRate
)
{
    public ParkingTariff ToEntity()
    {
        return new ParkingTariff()
        {
            Name = Name,
            FreeParkingDuration = TimeSpan.FromMinutes(FreeMinutes),
            HourlyRate = HourlyRate,
            DailyMaxRate = DailyMaxRate,
            IsActive = false,
        };
    }
}