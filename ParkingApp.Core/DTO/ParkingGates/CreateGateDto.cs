using ParkingApp.Core.Entities;
using ParkingApp.Core.Enums;

namespace ParkingApp.Core.DTOs.ParkingGates;

public record CreateGateDto(
    string Name,
    string Type,
    string Location
)
{
    public ParkingGate ToEntity()
    {
        return new ParkingGate()
        {
            Name = Name,
            Type = Enum.Parse<GateType>(Type, ignoreCase: true),
            Location = Location,
            IsOperational = true,
        };
    }
}