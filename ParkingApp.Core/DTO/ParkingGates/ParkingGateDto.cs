using ParkingApp.Core.Entities;
using ParkingApp.Core.Enums;

namespace ParkingApp.Core.DTOs.ParkingGates;

public record ParkingGateDto(
    Guid Id,
    string Name,
    string Type,
    string Location,
    bool IsOperational
)
{
    public ParkingGate ToEntity()
    {
        return new ParkingGate()
        {
            Id = Id,
            Name = Name,
            Type = Enum.Parse<GateType>(Type, ignoreCase: true),
            Location = Location,
            IsOperational = IsOperational,
        };
    }
}
