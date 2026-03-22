namespace ParkingApp.Core.DTOs.ParkingGates;

public record ParkingGateDto(
    Guid Id,
    string Name,
    string Type,
    string Location,
    bool IsOperational
);
