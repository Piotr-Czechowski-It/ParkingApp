namespace ParkingApp.Core.DTOs.ParkingTariffs;

public record CreateTariffDto(
    string Name,
    int FreeMinutes,
    decimal HourlyRate,
    decimal DailyMaxRate
);