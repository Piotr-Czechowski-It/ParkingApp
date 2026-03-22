namespace ParkingApp.Core.DTO.ParkingGates;

public record ParkingGateDto(
    Guid Id,
    string Name,
    string Type,
    string Location,
    bool IsOperational
);