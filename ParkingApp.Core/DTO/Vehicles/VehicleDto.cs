namespace ParkingApp.Core.DTOs.Vehicles;

public record VehicleDto(
    Guid Id,
    string LicensePlate,
    string Brand,
    string Color
);