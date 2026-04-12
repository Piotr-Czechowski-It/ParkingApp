using ParkingApp.Core.Entities;

namespace ParkingApp.Core.DTOs.Vehicles;

public record VehicleDto(
    Guid Id,
    string LicensePlate,
    string Brand,
    string Color
)
{
    public Vehicle ToEntity()
    {
        return new Vehicle()
        {
            Id = Id,
            LicensePlate = LicensePlate,
            Brand = Brand,
            Color = Color,
        };
    }
}